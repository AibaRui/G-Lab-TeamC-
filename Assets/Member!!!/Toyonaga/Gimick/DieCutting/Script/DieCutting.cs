using UnityEngine;

public class DieCutting : MonoBehaviour
{
    [SerializeField, Tooltip("DieCuttingRef�Q�[���I�u�W�F�N�g��o�^")]
    private DieCuttingRef dc_ref_;
    [SerializeField, Tooltip("�{�M�~�b�N�U�����A�L��������Q�[���I�u�W�F�N�g��o�^")]
    private GameObject awake_object_;
    [SerializeField, Tooltip("�\�������摜��o�^")]
    private Sprite im_snow_;
    [SerializeField, Tooltip("�\�����鐅�摜��o�^")]
    private Sprite im_water_;
    [SerializeField, Tooltip("�摜���m�̊Ԋu���J����ꍇ�A�l���w��")]
    float im_span_ = 0f;
    [SerializeField, Tooltip("�Q�Ƃƃu���b�N�̏�Ԃ����������Ftrue�ƂȂ�(getter�p�Ӎ�)")]
    bool is_complete_ = false;
    [SerializeField, Tooltip("���I�[���̃^�O�����w�肵�ĉ�����")]
    private string fire_Tag_name_ = "P1";
    [SerializeField, Tooltip("�X�I�[���̃^�O�����w�肵�ĉ�����")]
    private string ice_Tag_name_ = "P2";
    [SerializeField, Tooltip("box���I�[���ɂ���ď�ԕω�����܂ł̎��Ԃ��w�肵�ĉ�����")]
    private float box_change_time_ = 1.0f;
    public bool Is_complete { get { return is_complete_; } }
    

    private static GameObject[,] boxes_;    // �摜�Q�[���I�u�W�F�N�g
    private DieCuttingBox[,] boxes_script_; // �摜�Q�[���I�u�W�F�N�g�̃X�N���v�g�擾
    private int[,] boxes_state_ref_;        // �Q�ƂƂ̈�v��Ԕ�r�p�z��
    private int[,] boxes_state_;            // �{DieCutting�̌��݂̏�Ԃ��i�[(�f�t�H���g�F��)    

    void Start()
    {
        // ---- �Q�ƃI�u�W�F�N�g�̈�v��Ԕ�r�p�̔z����擾���� ---- //
        boxes_state_ref_ = dc_ref_.Boxes_state;
        boxes_state_ = new int[boxes_state_ref_.GetLength(0), boxes_state_ref_.GetLength(1)];   // �z��T�C�Y���Q�ƂƑ�����
        // ---- ���Ɏq�I�u�W�F�N�g�����݂���ꍇ�F�폜 --- //
        DestroyChildren(this.gameObject);
        // ---- int[,]�z�������ɉ摜�z��`�� ---- //
        boxes_ = new GameObject[boxes_state_.GetLength(0), boxes_state_.GetLength(1)];
        boxes_script_ = new DieCuttingBox[boxes_state_.GetLength(0), boxes_state_.GetLength(1)];
        for (var r = 0; r < boxes_state_.GetLength(0); r++)
        {
            for (var c = 0; c < boxes_state_.GetLength(1); c++)
            {
                // --- box�̏�Ԃ��(0)�ŏ����� --- //
                boxes_state_[r, c] = 0;                                     // ���ŏ������������ꍇ�A1�Ƃ���
                boxes_[r, c] = new GameObject($"Box({r},{c})");
                boxes_[r, c].transform.parent = transform;                  // �q�I�u�W�F�N�g�ɂ���
                // -- �摜�ݒ� -- //
                var sprite = boxes_[r, c].AddComponent<SpriteRenderer>();   // �摜�\���̂��߃����_�[��^����
                if (boxes_state_[r, c] == 0) { sprite.sprite = im_snow_; }  // �摜��^����
                else if (boxes_state_[r, c] == 1) { sprite.sprite = im_water_; }
                else { Debug.Log("Error : �� or ���摜�� 0 or 1 �̐����Ŏw�肵�ĉ�����"); }
                // -- �X�N���v�g�������o�ϐ���^����ݒ� -- //
                var script = boxes_[r, c].AddComponent<DieCuttingBox>();
                script.Ice_Tag_name = ice_Tag_name_;                        // �I�[���Ɣ�r����^�O����^����
                script.Fire_Tag_name = fire_Tag_name_;                      // 
                script.State = 0;                                           // ���ŏ������������ꍇ�A�P�Ƃ���
                script.SetSnowImg = im_snow_;
                script.SetWaterImg = im_water_;
                script.ChangeTime = box_change_time_;                       // 
                boxes_script_[r, c] = script;                               // �X�N���v�g�̎Q�Ǝ擾
                // -- �R���C�_�[�ݒ� -- //
                var collider = boxes_[r, c].AddComponent<BoxCollider2D>();  // �����蔻��p�ɃR���C�_�[��^����
                collider.size = new Vector2(sprite.size.x, sprite.size.y);  // �R���C�_�[�T�C�Y���摜�T�C�Y�ɑ�����
                boxes_[r, c].transform.position = new Vector3(              // �e���W����̈ړ������w��
                    transform.position.x + c * (sprite.size.x + im_span_),
                    transform.position.y - r * (sprite.size.y + im_span_),
                    transform.position.z);
                
            }
        }
    }

    void Update()
    {
        // ---- ��x�ł��R���v���[�g�t���O����������A�{�M�~�b�N�͓���s�v
        if (is_complete_) { return; };
        // ---- box�̏�ԕω�(bool is_type_changed)���m�̓x�Aboxes_state���A�b�v�f�[�g ---- //
        bool stateChanged = false;
        for (var r = 0; r < boxes_.GetLength(0); r++)
        {
            for (var c = 0; c < boxes_.GetLength(1); c++ )
            {
                if (boxes_script_[r, c].IsStateChanged)
                {
                    boxes_state_[r, c] = boxes_script_[r, c].State;
                    stateChanged = true;
                }
            }
        }
        if (stateChanged)
        {
            // --- ref : boxes_state_ref�ƌ��݂̏�Ԃ��r --- //
            bool isMached = true;
            for(var r = 0; r < boxes_state_ref_.GetLength(0); r++)
            {
                for(var c = 0; c < boxes_state_ref_.GetLength(1); c++)
                {
                    if(boxes_state_ref_[r,c] != boxes_state_[r, c]) { isMached = false; break; }
                }
            }
            // --- �Q�Ƃƃu���b�N�̏�Ԃ����������F�R���v���[�g�t���O�𗧂Ă� --- //
            if (isMached) { is_complete_ = true; }
        }
        
    }

    private void DestroyChildren(GameObject parent)
    {
        for (var i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            child.parent = null;
            GameObject.Destroy(child.gameObject);
        }
    }
}
