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
    private enum mode_
    {
        circle, front_box, up_box, back_box, down_box, end
    }
    [SerializeField, Tooltip("�{���[�h���w�肷�鎖�ŁA�I�[���`��ɉ����ăp�[�e�B�N�����ړ�������")]
    private mode_ aura_mode_ = mode_.up_box;
    private mode_ current_aura_;            // ���݂̃��[�h�i�[
    private mode_ pre_aura_;                // 1�t���[���O�̃��[�h�i�[

    private BoxCollider2D p_boxColl_;
    private CircleCollider2D p_circleColl_;
    private int current_mode_;
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
        float rotate_length_;
        int rotate_dir_;
        public Quaternion rot_;
    }
    private particle[,] particle_st_;


    private void Start()
    {
        // ---- ���������� ---- //
        p_boxColl_ = p_boxArea_.GetComponent<BoxCollider2D>();
        p_circleColl_ = p_circleArea_.GetComponent<CircleCollider2D>();
        current_mode_ = (int) aura_mode_;       // �I�[���̏�����Ԃ��C���X�y�N�^����擾
        current_aura_ = aura_mode_;             // �I�[���̌��݂̏�Ԃ��i�[
        // --- �p�[�e�B�N������ --- //
        particle_st_ = GenerateParticles();
        // --- ���̑��A���[�h�`�F���W���ɕK�v�ȃp�����[�^���擾���Ă��� --- //

        



    }

    private void Update()
    {
        // --- test --- //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            current_aura_ += 1;
            if (current_aura_ == mode_.end) { current_aura_ = mode_.circle; }

        };
        if (ModeChange())
        {
            Debug.Log(current_aura_);
        }

    }

    private void FixedUpdate()
    {
        // ---- ���s���̏��� ---- //


        ParticleRandomUpdate_Box(ref particle_st_, ref current_aura_, Time.fixedDeltaTime);
        // --- �p�[�e�B�N���ʒu�̃A�b�v�f�[�g --- //
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
        //mode_ temp_mode = md;               // ���݂̃��[�h���i�[���Ă���
        current_mode_ = (int)mode_.up_box;  // ���[�hup�ŏ������������s���܂�
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
                p[r, c].rot_ = Quaternion.AngleAxis(Mathf.Deg2Rad * 90, transform.forward);
                p[r,c].pm_.TargetPos = p[r,c].position_;
            }
        }
        return p;
    }

    private void ParticleRandomUpdate_Box(ref particle[,] p, ref mode_ m, float delta_time)
    {
        // ---- �p�[�e�B�N���������_���ɓ�����: BoxCollider�Ώ� ---- //
        rand_time_counter_ += delta_time;
        if (rand_time_counter_ < rand_time_) { return; }
        rand_time_counter_ = 0;
        if(m != mode_.front_box && m!= mode_.up_box && m!= mode_.back_box && m!= mode_.down_box) { return; }
        // --- �p�[�e�B�N���̖ڕW�ʒu�X�V --- //
        float box_width = p_boxColl_.size.x;
        float box_height = p_boxColl_.size.y;
        float box_cell_width = box_width / p.GetLength(0);
        float box_cell_height = box_height / p.GetLength(1);
        float x0 = p_boxColl_.transform.position.x - box_width / 2;       // box�R���C�_�[����̍��W����Ƀp�[�e�B�N������
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
                Vector3 delta_pos = (p_boxColl_.transform.position - transform.position) +  Quaternion.Euler(0, 0, deg) * delta_pos_to_box;
                p[r, c].delta_position_ = delta_pos; 
            }
        }

    }


    private bool ModeChange()
    {
        // ---- �I�[���̃��[�h��ύX���� ---- //
        if (current_aura_ == pre_aura_) { return false; };    // ���[�h�ω���������ΏI��
        
        // --- �I�[�����~�̏ꍇ --- //
        if(current_aura_ == mode_.circle)
        {
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
