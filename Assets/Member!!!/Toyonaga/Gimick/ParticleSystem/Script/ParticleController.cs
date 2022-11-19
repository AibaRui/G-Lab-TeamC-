using UnityEngine;
using System.Collections.Generic;

public class ParticleController : GimickBase
{
    [Header("�|�[�Y��True"), SerializeField]
    public bool is_Pause_ = false;
    [Header("���~�I�[��"), SerializeField]
    private List<CircleCollider2D> p_cir_areas_;
    [Header("���l�p�I�[��")]
    [SerializeField, Tooltip("�R���C�_�[��y�������ꂼ��̕����ɉ�]���������̂�o�^���ĉ�����")]
    private List<BoxCollider2D> p_box_areas_;
    [Header("���I�[���ύX����: �~�F0�X�^�[�g")]
    [SerializeField, Tooltip("�~�I�[������0, ���̌�A�~�I�[����List���A�l�p�I�[����List���ɉ����Ďw�肷��")]
    public int mode_num_ = 0;       // �I�[���̃��[�h���Ǘ�����ϐ�
    [Header("���������鐸��(�p�[�e�B�N��)��o�^"), SerializeField]
    private GameObject p_obj_;
    [Header("���p�[�e�B�N���̍s�̐�"), SerializeField]
    private int p_row_ = 2;
    [Header("���p�[�e�B�N���̗�̐�"), SerializeField]
    private int p_col_ = 5;
    [Header("Setting")]
    [SerializeField, Tooltip("�p�[�e�B�N���̂΂���[����, 1 = 100%]")]
    private float rand_coff_ = 0.5f;
    [SerializeField, Tooltip("�p�[�e�B�N���̂΂�����ԊԊu[s]")]
    private float rand_time_ = 0.2f;
    private float rand_time_counter = 0;
    [SerializeField, Tooltip("�p�[�e�B�N����z�u���Ȃ��~�̑傫�����w�肷��")]
    private float p_cicle_voidArea_size_ = 0.2f;
    private enum mode_              // �~ or �l�p��
    {
        circle, box, end
    }
    private mode_ aura_mode_ = mode_.circle;

    private struct particle         // �p�[�e�B�N�������ꊇ�Ǘ�
    {
        public GameObject particle_;
        public ParticleMove pm_;
        public Vector3 position_;
        public Vector3 delta_position_;
    }
    private particle[,] p_st_;

    private void Start()
    {
        // ---- ���������� ---- //
        p_st_ = GenerateParticles(ref aura_mode_);
    }

    private void FixedUpdate()
    {
        // ---- �|�[�Y���͓��얳�� ---- //
        if (is_Pause_) { return; }
        // ---- ���s���̏��� ---- //
        ChangeAura(ref aura_mode_, ref mode_num_);     // �C���X�y�N�^�Fmode_num_�̕ύX�ɉ����ăI�[���̏�ԕω�
        if (aura_mode_ == mode_.circle) { ParticleRandomUpdate_Circle(ref p_st_, ref mode_num_, Time.deltaTime); }
        if (aura_mode_ == mode_.box) { ParticleRandomUpdate_Box(ref p_st_, ref mode_num_, Time.deltaTime); }
        // ---- �p�[�e�B�N���ʒu�̃A�b�v�f�[�g�F�e(ParticleSystem)���W����̕΍��ʒu�Fp_st.delta_position���g�p���čX�V --- //
        for (int r = 0; r < p_row_; r++)
        {
            for (int c = 0; c < p_col_; c++)
            {
                p_st_[r, c].pm_.TargetPos = transform.position + p_st_[r, c].delta_position_;
            }
        }
    }

    private particle[,] GenerateParticles(ref mode_ m)
    {
        // ---- �p�[�e�B�N�����Q�[���I�u�W�F�N�g�͈͓��ɐ��� ---- //
        particle[,] p = new particle[p_row_, p_col_];
        mode_ temp_mode = m;                    // ���݂̃��[�h��ۑ����Ă���
        m = mode_.box;                          // �{�b�N�X���[�h�Ő������{
        if (p_box_areas_[0] == null) { Debug.Log("Error, Please Set to Box Collider"); }
        BoxCollider2D box = p_box_areas_[0];    // �o�^�����{�b�N�X�R���C�_�[���ꎞ�擾
        // ---- p_st�̏����쐬���Ă��� ---- //
        Vector3 box_d_pos = transform.position - box.transform.position;    // �e����{�b�N�X�܂ł̋����擾
        float box_width = box.size.x;
        float box_height = box.size.y;
        float box_cell_width = box.size.x / p_row_;
        float box_cell_height = box.size.y / p_col_;
        float x0 = box.transform.position.x - box_width / 2;        // y�������𐳂Ƃ��Abox�R���C�_����̍��W��Ƀp�[�e�B�N������
        float y0 = box.transform.position.y - box_height / 2;
        for (int r = 0; r < p_row_; r++)
        {
            for (int c = 0; c < p_col_; c++)
            {
                // --- �ʒu��񌈒� --- //
                float pos_x = x0 + r * box_cell_width + box_cell_width / 2;
                float pos_y = y0 - c * box_cell_height - box_cell_height / 2;
                // �z�u�Ƀ����_�����𓱓�����
                pos_x += Random.Range(-box_cell_width / 2 * rand_coff_, box_cell_width / 2 * rand_coff_);
                pos_y += Random.Range(-box_cell_height / 2 * rand_coff_, box_cell_height / 2 * rand_coff_);
                // --- �Q�[���I�u�W�F�N�g�̃C���X�^���X�����w�� --- //
                var obj = Instantiate(p_obj_);
                obj.transform.parent = transform;   // �e��{�Q�[���I�u�W�F�N�g�ɐݒ�
                obj.transform.position = new Vector3(pos_x, pos_y, transform.position.z);   // box�R���C�_�[�w��̈ʒu�Ɉړ�
                // --- �\���̂ɏ����i�[���Ă��� --- //
                p[r, c].particle_ = obj;
                p[r, c].pm_ = obj.GetComponent<ParticleMove>();
                p[r, c].position_ = new Vector3(pos_x, pos_y, transform.position.z);
                p[r, c].delta_position_ = p[r, c].position_ - transform.position;
                p[r, c].pm_.TargetPos = p[r, c].position_;
            }
        }
        m = temp_mode;          // ���[�h��߂�

        return p;
    }

    private void ParticleRandomUpdate_Box(ref particle[,] p, ref int mode_num, float delta_time)
    {
        // ---- �p�[�e�B�N���������_���ɓ��������߁A����ڕW�ʒu�̊�ƂȂ�΍��ʒu�����쐬���i�[: BoxCollider2D�Ώ� ---- //
        // --- �����_���^�C���J�E���g --- //
        rand_time_counter += delta_time;
        if (rand_time_counter < rand_time_) { return; }
        rand_time_counter = 0;
        // --- �p�[�e�B�N���̖ڕW�ʒu�X�V --- //
        int box_num = mode_num - p_cir_areas_.Count;
        BoxCollider2D box = p_box_areas_[box_num];      // �p�[�e�B�N���X�V�Ώۂ��ꎞ�Q��
        float box_width = box.size.x;
        float box_height = box.size.y;
        float box_cell_width = box_width / p.GetLength(0);
        float box_cell_height = box_height / p.GetLength(1);
        float local_x0 = -box_width / 2;
        float local_y0 = box_height / 2;
        for (int r = 0; r < p.GetLength(0); r++)
        {
            for (int c = 0; c < p.GetLength(1); c++)
            {
                // �{�b�N�X���[�J�����W�Ńp�[�e�B�N���̖ڕW�ʒu�v�Z
                Vector3 local_pos = new Vector3(
                    local_x0 + r * box_cell_width + box_cell_width / 2,
                    local_y0 - c * box_cell_height - box_cell_height / 2,
                    transform.position.z
                    );
                // �{�b�N�X���[�J�����W�F�z�u�Ƀ����_�����𓱓�����
                local_pos += new Vector3(
                    Random.Range(-box_cell_width / 2 * rand_coff_, box_cell_width / 2 * rand_coff_),
                    Random.Range(-box_cell_height / 2 * rand_coff_, box_cell_height / 2 * rand_coff_),
                    0
                    );
                // �\���̂ɏ����i�[���Ă����F�e����p�[�e�B�N���̖ڕW�ʒu�̕΍������擾���Ă����΁A���W�X�V���o����
                float deg = box.transform.eulerAngles.z;    // �{�b�N�X��]��(deg)�擾
                Vector3 delta_pos = (box.transform.position - transform.position) + Quaternion.Euler(0, 0, deg) * local_pos;
                p[r, c].delta_position_ = delta_pos;
            }
        }
    }

    private void ParticleRandomUpdate_Circle(ref particle[,] p, ref int mode_num, float delta_time)
    {
        // ---- �p�[�e�B�N���������_���ɓ��������߁A����ڕW�ʒu�̊�ƂȂ�΍��ʒu�����쐬���i�[�FCircleCollider2D�Ώ� ---- //
        // --- �����_���^�C���J�E���g --- //
        rand_time_counter += delta_time;
        if (rand_time_counter < rand_time_) { return; }
        rand_time_counter = 0;
        // --- �p�[�e�B�N���̖ڕW�ʒu�X�V --- //
        Vector3 particle_delta_pos_ = new Vector3(0, 0, 0);       // �ꎞ�i�[�ϐ�
        int i = 0;
        float deg = 0;                                                  // �p�[�e�B�N����z�u����p�x
        float d_deg = 360 / p.GetLength(0) / p.GetLength(1);   // �p�[�e�B�N����z�u���鍏�݊p�x
        for (int r = 0; r < p.GetLength(0); r++)
        {
            for (int c = 0; c < p.GetLength(1); c++)
            {
                // -- �p�[�e�B�N���~�`�����Ap_circle_void_Area_size���傫���ʒu�Ƀp�[�e�B�N���������_���ɔz�u���Ă��� -- //
                float cir_span = p_cir_areas_[mode_num].radius - p_cicle_voidArea_size_ / 2;
                particle_delta_pos_ = transform.right * (cir_span / 2 + p_cicle_voidArea_size_ / 2);
                // - �z�u�Ƀ����_�������������� - //
                particle_delta_pos_ += transform.right * Random.Range(-cir_span * rand_coff_, cir_span * rand_coff_);
                // - ��] - //
                particle_delta_pos_ = Quaternion.Euler(0, 0, deg) * particle_delta_pos_;

                p[r, c].delta_position_ = particle_delta_pos_;
                deg += d_deg;
                i++;    // 1�����z��̎Q�Ɛ�X�V
            }
        }
    }

    private void ChangeAura(ref mode_ m, ref int mode_num)
    {
        // ---- ���[�h�`�F���W ---- //
        int cir_num = p_cir_areas_.Count;
        int box_num = p_box_areas_.Count;
        if (cir_num + box_num <= mode_num)
        {
            mode_num = 0;
            m = mode_.circle;
        }
        if (mode_num < cir_num) { m = mode_.circle; }
        if (mode_num >= cir_num) { m = mode_.box; }
    }

    public override void GameOverPause()
    {
        is_Pause_ = true;
    }

    public override void Pause()
    {
        is_Pause_ = true;
    }

    public override void Resume()
    {
        is_Pause_ = false;
    }

}