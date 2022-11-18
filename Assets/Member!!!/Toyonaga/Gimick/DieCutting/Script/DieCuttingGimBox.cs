using UnityEngine;

public class DieCuttingGimBox : MonoBehaviour
{
    // ----- �M�~�b�N�u���b�N�̏�ԓ����Ǘ����� ----- //
    private bool is_state_changed_ = false;
    public bool Is_state_changed { get { return is_state_changed_; } }
    private int state_;
    public int State { get { return state_; } set { state_ = value; } }
    private Sprite snow_img_;
    public Sprite Snow_img { get { return snow_img_; } set { snow_img_ = value; } }
    private Sprite water_img_;
    public Sprite Water_img { get { return water_img_; } set { water_img_ = value; } }
    private string fire_tag_name_;
    public string Fire_tag_name { get { return fire_tag_name_; } set { fire_tag_name_ = value; } }
    private string ice_tag_name_;
    public string Ice_tag_name { get { return ice_tag_name_; } set { ice_tag_name_ = value; } }
    private float box_change_time_ = 1f;
    public float Box_change_time { get { return box_change_time_; } set { box_change_time_ = value;} }
    
    SpriteRenderer Sp;
    private bool is_other_enterd_ = false;  // ���̃R���C�_�[���N�������ǂ����̔���
    private float count_snow_ = 0;          // �I�[���̉e������(��)���J�E���g
    private float count_water_ = 0;         // �I�[���̉e������(�X)���J�E���g

    private void Start()
    {
        Sp = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // ---- �I�[���̌��m ---- //
        if (!is_other_enterd_)
        {
            if (count_snow_ > 0) { count_water_ -= Time.fixedDeltaTime; }
            if(count_water_ > 0) { count_snow_ -= Time.fixedDeltaTime; }
        }
        // ---- ��ԕω��t���O�����m�F�摜�ύX��A�t���O�����Z�b�g ---- //
        if (!is_state_changed_) { return; } // ��ԕω���������ΏI��
        if (state_ == 0) { Sp.sprite = snow_img_; }
        else if (state_ == 1) { Sp.sprite = water_img_; }
        is_state_changed_ = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ---- �{�b�N�X�̏��(�X/��)��ύX ---- //
        // ---- ��ԕύX ---- //
        is_other_enterd_ = true;
        if (other.transform.CompareTag(fire_tag_name_))
        {
            count_water_ += Time.deltaTime;
            if(count_water_ >= box_change_time_)
            {
                is_state_changed_ = true;
                state_ = 1;
            }
        } else if (other.transform.CompareTag(ice_tag_name_))
        {
            count_snow_ += Time.deltaTime;
            if(count_snow_ >= box_change_time_)
            {
                is_state_changed_ = true;
                state_ = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ---- �{�b�N�X������ɃI�u�W�F�N�g�̗L�����m�F ---- //   
        is_other_enterd_ = false;
    }


}
