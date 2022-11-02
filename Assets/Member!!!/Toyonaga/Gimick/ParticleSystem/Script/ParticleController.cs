using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField, Tooltip("������BOX�I�[�����w�肷��(���ȉ��Aaura_mode�Ə�Ԃ����킹�鎖")]
    private GameObject p_boxArea_;
    [SerializeField, Tooltip("�����̉~�I�[�����w�肷��")]
    private GameObject p_circleArea_;
    [SerializeField, Tooltip("�p�[�e�B�N����z�u���Ȃ��~�̑傫�����w�肷��")]
    private float p_cicle_voidArea_size_;
    [SerializeField, Tooltip("��������p�[�e�B�N���I�u�W�F�N�g���w��")]
    private GameObject p_object_;
    private ParticleMove[,] p_obj_scripts_;
    [SerializeField, Tooltip("�p�[�e�B�N���̗�̐�")]
    private int p_row_ = 5;
    [SerializeField, Tooltip("�p�[�e�B�N���̍s�̐�")]
    private int p_col_ = 2;
    [SerializeField, Tooltip("�p�[�e�B�N�����ǂꂾ���΂����^���邩�̌W��(0 ~ 1(%)")]
    private float rand_coff_ = 0.5f;
    [SerializeField, Tooltip("�p�[�e�B�N�����΂�����鎞�ԊԊu��ݒ�(s)")]
    private float rand_time_ = 0.2f;
    private float rand_time_counter_ = 0;
    public enum mode_
    {
        circle, front_box, up_box, back_box, down_box, end
    }
    [SerializeField, Tooltip("�{���[�h���w�肷�鎖�ŁA�I�[���`��ɉ����ăp�[�e�B�N�����ړ�������")]
    private mode_ aura_mode_ = mode_.up_box;
    private  mode_ current_aura_;            // ���݂̃��[�h�i�[
    private mode_ pre_aura_;                // 1�t���[���O�̃��[�h�i�[
    public mode_ change_aura_;              // �O������Q�Ɖ\�ȃ��[�h��p��

    private BoxCollider2D p_boxColl_;
    private CircleCollider2D p_circleColl_;
    private Vector3 boxArea_d_pos_;         // �v���C���[���_ to �p�[�e�B�N��Box���S�܂ł̋����x�N�g���擾
    private Vector3[,] p_delta_pos_;        // �v���C���[���_����p�[�e�B�N��i�܂ł̋������i�[���Ă���
    private Vector3[,] p_target_pos_world_; // �p�[�e�B�N���̖ڕW�ʒu(���[���h���W) 

    private Quaternion box_rot_;            // �v���C���[����

    private struct particle
    {
        // �p�[�e�B�N�������ꊇ�Ǘ�
        public GameObject particle_;
        public ParticleMove pm_;
        public Vector3 position_;
        public Vector3 delta_position_;
    }
    private particle[,] particle_st_;

    // ------------------------ //
    // ---- �O���Q�Ɨp�֐� ---- //
    public void SetAuraChange(mode_ m)
    {
        // --- �I�[���̏�Ԃ��w��F�p�[�e�B�N�������͈̔͂ɒǏ]���� --- //
        change_aura_ = m;
        if (change_aura_ >= mode_.end) { change_aura_ = mode_.circle; }
        ModeChange(ref change_aura_);
    }

    public void SetAuraChangeRow()
    {
        // ---- �{�֐����Ăяo���x�A�I�[���̏�Ԃ��ꑗ�肷�� ---- //
        change_aura_++;
        if (change_aura_ >= mode_.end) { change_aura_ = mode_.circle; }
        ModeChange(ref change_aura_);
    }
    // ------------------------- //

    private void Start()
    {
        // ---- ���������� ---- //
        p_boxColl_ = p_boxArea_.GetComponent<BoxCollider2D>();
        p_circleColl_ = p_circleArea_.GetComponent<CircleCollider2D>();
        current_aura_ = aura_mode_;       // �I�[���̏�����Ԃ��C���X�y�N�^����擾
        change_aura_ = aura_mode_;
        // --- �p�[�e�B�N������ --- //
        particle_st_ = GenerateParticles();
    }


    private void FixedUpdate()
    {
        // ---- ���s���̏��� ---- //
        ParticleRandomUpdate_Box(ref particle_st_, ref current_aura_, Time.fixedDeltaTime);     // �p�[�e�B�N�������[�h�ɉ�����BOX���ɕ��V������
        ParticleRandomUpdate_Circle(ref particle_st_, ref current_aura_, Time.fixedDeltaTime);  // �p�[�e�B�N�������[�h�ɉ�����CIRCLE���ɕ��V������
        // --- �p�[�e�B�N���ʒu�̃A�b�v�f�[�g�F�e�iParticleSystem)�̈ʒu�ɉ����ăp�[�e�B�N���ʒu���X�V�������� --- //
        for (int r = 0; r < p_row_; r++)
        {
            for(int c = 0; c < p_col_; c++)
            {
                particle_st_[r, c].pm_.TargetPos = transform.position + particle_st_[r, c].delta_position_;
            }
        }
    }

    private particle[,] GenerateParticles()
    {
        // ---- �p�[�e�B�N���Q�[���I�u�W�F�N�g��͈͓��ɐ��� ---- //
        particle[,] p = new particle[p_row_, p_col_];
        mode_ temp_mode = current_aura_;                 // ���݂̃��[�h���i�[���Ă���
        current_aura_ = mode_.up_box;                    // ���[�hup�ŏ������������s���܂�
        Vector3 box_pos = transform.position - p_boxColl_.transform.position;   // �v���C���[���_����Box�R���W�����܂ł̋����x�N�g���擾
        float box_width = p_boxColl_.size.x;
        float box_height = p_boxColl_.size.y;
        float box_length = box_pos.magnitude;   // �������Ƃ��Ċi�[
        float box_cell_width_ = box_width / p_row_;
        float box_cell_height_ = box_height / p_col_;

        float x0 = p_boxColl_.transform.position.x - box_width / 2;       // box�R���C�_�[����̍��W����Ƀp�[�e�B�N������
        float y0 = p_boxColl_.transform.position.y + box_height / 2;

        for(int r = 0; r < p_row_; r++)
        {
            for(int c = 0; c < p_col_; c++)
            {
                // �ʒu��񌈒�
                float pos_x = x0 + r * box_cell_width_ + box_cell_width_ / 2;
                float pos_y = y0 - c * box_cell_height_ - box_cell_height_ / 2;
                // �z�u�Ƀ����_�����𓱓�����
                pos_x += Random.Range(-box_cell_width_/2 * rand_coff_, box_cell_width_/2 * rand_coff_);
                pos_y += Random.Range(-box_cell_height_/2 * rand_coff_, box_cell_height_/2 * rand_coff_);

                // --- �Q�[���I�u�W�F�N�g�̃C���X�^���X�����w�� --- //
                var obj = Instantiate(p_object_);
                obj.transform.parent = transform;   // �e��{�Q�[���I�u�W�F�N�g�ɐݒ�
                obj.transform.position = new Vector3(pos_x, pos_y, transform.position.z);   // box�R���C�_�[�w��̈ʒu�Ɉړ�
                // --- �\���̂ɏ����i�[���Ă��� --- //
                p[r, c].particle_ = obj;
                p[r, c].pm_ = obj.GetComponent<ParticleMove>();
                p[r, c].position_ = new Vector3(pos_x, pos_y, transform.position.z);
                p[r, c].delta_position_ = p[r, c].position_  - transform.position;
                p[r,c].pm_.TargetPos = p[r,c].position_;
            }
        }
        // ���[�h�����̏�Ԃɖ߂�
        current_aura_ = temp_mode;

        return p;
    }

    private void ParticleRandomUpdate_Box(ref particle[,] p, ref mode_ m, float delta_time)
    {
        // ---- �p�[�e�B�N���������_���ɓ�����: BoxCollider�Ώ� ---- //
        // --- �{�֐��ɗ^����ꂽ���[�h�FBox�ȊO�Ȃ�΂����ŏI�� --- //
        if (m != mode_.front_box && m != mode_.up_box && m != mode_.back_box && m != mode_.down_box) { return; }
        // --- �����_���^�C���J�E���g --- //
        rand_time_counter_ += delta_time;
        if (rand_time_counter_ < rand_time_) { return; }
        rand_time_counter_ = 0;
        // --- �p�[�e�B�N���̖ڕW�ʒu�X�V --- //
        float box_width = p_boxColl_.size.x;
        float box_height = p_boxColl_.size.y;
        float box_cell_width = box_width / p.GetLength(0);
        float box_cell_height = box_height / p.GetLength(1);
        float x0 = p_boxColl_.transform.position.x - box_width / 2;       // box�R���C�_�[����̍��W����Ƀp�[�e�B�N���ʒu�ύX
        float y0 = p_boxColl_.transform.position.y + box_height / 2;
        float deg = p_boxColl_.transform.localEulerAngles.z;
        for (int r = 0; r < p.GetLength(0); r++)
        {
            for( int c = 0; c < p.GetLength(1); c++)
            {
                // �ʒu��񌈒�
                float pos_x = x0 + r * box_cell_width + box_cell_width / 2;
                float pos_y = y0 - c * box_cell_height - box_cell_height / 2;
                // �z�u�Ƀ����_�����𓱓�����
                pos_x += Random.Range(-box_cell_width / 2 * rand_coff_, box_cell_width / 2 * rand_coff_);
                pos_y += Random.Range(-box_cell_height / 2 * rand_coff_, box_cell_height / 2 * rand_coff_);
                // �\���̂ɏ����i�[���Ă���
                Vector3 delta_pos_to_box = new Vector3(pos_x, pos_y, transform.position.z) - p_boxColl_.transform.position;
                // ��]�𔽉f
                Vector3 delta_pos = (p_boxColl_.transform.position - transform.position) +  Quaternion.Euler(0, 0, deg) * delta_pos_to_box;
                p[r, c].delta_position_ = delta_pos; 
            }
        }
    }

    private void ParticleRandomUpdate_Circle(ref particle[,] p, ref mode_ m, float delta_time)
    {
        // ---- �p�[�e�B�N���������_���ɓ�����: CircleCollider�Ώ� ---- //
        // --- �{�֐��ɗ^����ꂽ���[�h�FCircle�ȊO�Ȃ�΂����ŏI�� --- //
        if (m != mode_.circle) { return; }
        // --- �����_���^�C���J�E���g --- //
        rand_time_counter_ += delta_time;
        if (rand_time_counter_ < rand_time_) { return; }
        rand_time_counter_ = 0;
        // --- �p�[�e�B�N���̖ڕW�ʒu�X�V --- //
        Vector3 particle_delta_pos_ = new Vector3(0, 0, 0);       // �ꎞ�i�[�ϐ�
        int i = 0;
        float deg = 0;                                                  // �p�[�e�B�N����z�u����p�x
        float d_deg = 360 / p.GetLength(0) / p.GetLength(1);   // �p�[�e�B�N����z�u���鍏�݊p�x
        for (int r = 0; r < p.GetLength(0); r++)
        {
            for(int c = 0; c < p.GetLength(1); c++)
            {
                // -- �p�[�e�B�N���~�`�����Ap_circle_void_Area_size���傫���ʒu�Ƀp�[�e�B�N���������_���ɔz�u���Ă��� -- //
                float cir_span = p_circleColl_.radius - p_cicle_voidArea_size_ / 2; 
                particle_delta_pos_ = transform.right * (cir_span /2 + p_cicle_voidArea_size_/2);
                // - �z�u�Ƀ����_�������������� - //
                particle_delta_pos_ += transform.right * Random.Range(-cir_span * rand_coff_, cir_span * rand_coff_);
                // - ��] - //
                particle_delta_pos_ = Quaternion.Euler(0, 0, deg) *  particle_delta_pos_;

                p[r, c].delta_position_ = particle_delta_pos_;
                deg += d_deg;
                i++;    // 1�����z��̎Q�Ɛ�X�V
            }
        }
                                                                                            
    }


    private bool ModeChange(ref mode_ m)
    {
        current_aura_ = m;
        // ---- �I�[���̃��[�h��ύX���� ---- //
        if (current_aura_ == pre_aura_) { return false; };    // ���[�h�ω���������ΏI��
        
        // --- �I�[�����~�̏ꍇ --- //
        if(current_aura_ == mode_.circle)
        {
            // ���݂̎d�l�F�~�I�[���̓v���C���[�ʒu�ɌŒ�FCIRCLE�R���C�_�[�̈ʒu�E�p���ω��F���ɕK�v�ȏ�������
            return true;                                      // �����ŏ����I��
        }
        // --- �I�[�����l�p�`�̏ꍇ --- //
        Vector3 box_pos_length = transform.position - p_boxColl_.transform.position;
        Vector3 box_dir = new Vector3(0, 0, 0);
        float box_pos_sq = box_pos_length.magnitude;
        float box_deg = 0;

        // -- Box�̊p�x�ƕ��������� -- // 
        if (current_aura_ == mode_.front_box) {
            box_deg = -90;
            box_dir = transform.right;
        }
        if(current_aura_ == mode_.up_box) { 
            box_deg = 0; 
            box_dir = transform.up;
        }
        if(current_aura_ == mode_.back_box)
        {
            box_deg = 90;
            box_dir = -1 * transform.right;
        }
        if(current_aura_ == mode_.down_box)
        {
            box_deg = 0;
            box_dir = -1 * transform.up;
        }
       
        p_boxColl_.transform.rotation = Quaternion.Euler(0, 0, -box_deg);
        p_boxColl_.transform.localPosition = box_dir * box_pos_sq;
        // 1�t���[���O�̃��[�h�i�[
        pre_aura_ = current_aura_;                      

        return true;
    }


}
