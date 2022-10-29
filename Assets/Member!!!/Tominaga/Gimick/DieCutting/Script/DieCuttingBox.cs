using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCuttingBox : MonoBehaviour
{
    [SerializeField]
    private Sprite im_snow_;
    public Sprite SetSnowImg { set{ im_snow_ =value; }}
    [SerializeField]
    private Sprite im_water_;
    public Sprite SetWaterImg { set{ im_water_ = value; }}
    [SerializeField]
    private string ice_Tag_name_;
    public string Ice_Tag_name { set { ice_Tag_name_ = value; }}
    [SerializeField]
    private string fire_Tag_name_;
    public string Fire_Tag_name { set { fire_Tag_name_ = value; }}
    [SerializeField]
    private int state_ = 0;               // ��F0, ���F1
    public int State { get { return state_; } set { state_ = value; }}

    [SerializeField]
    private float change_time_ = 0.5f;  // Type�ω��܂ł̎���
    [SerializeField]
    public float ChangeTime { set { change_time_ = value;} }
    private bool is_state_changed = false;
    public bool IsStateChanged { get { return is_state_changed; } }
    [SerializeField]
    private float count_snow_ = 0;     // �I�[���̉e������(��)���J�E���g
    [SerializeField]
    private float count_water_ = 0;    // �I�[���̉e�����ԁi�X)���J�E���g

    private bool is_other_enterd = false;    // ���̃R���C�_�[���N�������ǂ����̔���

    [SerializeField]
    SpriteRenderer Sp;

    private void Start()
    {
        Sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // ---- �J�E���g�̃��Z�b�g ---- //
        if (!is_other_enterd)
        {
            if(count_water_ > 0) { count_water_ -= Time.deltaTime; }
            if(count_snow_ > 0) { count_snow_ -= Time.deltaTime; }
        }
        // ---- ��ԕω��t���O�����m�F�摜�ύX��A�t���O�����Z�b�g ---- //
        if(!is_state_changed) { return; }
        if(state_ == 0) { Sp.sprite = im_snow_; }
        else if(state_ == 1) { Sp.sprite = im_water_; }
        is_state_changed = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        is_other_enterd = true;
        if(other.transform.CompareTag(fire_Tag_name_))
        {
            count_water_ += Time.deltaTime;
            if(count_water_ >= change_time_)
            {
                is_state_changed = true;
                state_ = 1;
            }
        }else if (other.transform.CompareTag(ice_Tag_name_))
        {
            count_snow_ += Time.deltaTime;
            if(count_snow_ >= change_time_)
            {
                is_state_changed = true;
                state_ = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        is_other_enterd = false;
    }
}
