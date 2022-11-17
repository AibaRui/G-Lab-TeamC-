using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ----- �����̓����E����̓������Ǘ�����N���X ----- //
// ������(fountain_baseObj)�ׂ̍�������(�h��� / �I�[�����󂯂đ傫���Ȃ�/�������Ȃ�)�́AIcsBase.cs�ɔC����B
// ���C���F�X��(�����ɊW�����Ă���I�u�W�F�N�g�j��������Γ���B
//  -> �����摜Object(fountain_imgObj)�Ƒ���(fountain_baseObj)�������I�ɏ㉺�����A
//     �Ԍ��򂪐����o�� / �~�ޓ�����{�N���X�ŊǗ����Ă���B
//     FountainMove()�֐��ł��̓������������Ă���

public class Fountain : MonoBehaviour
{
    
    public float Fountain_height_ { get { return fountain_height_; } }
    [SerializeField, Header("������ON/OFF��������̕b���w��(s)")]
    private float fountain_time_ = 5f;
    [SerializeField, Header("�����̑��x���w��(����(1) / s)")]
    private float fountain_velocity_ = 3f;
    private float fountain_counter_ = 0f;   // ����ON/OFF�̎��Ԃ��J�E���g
    private bool up = false;                // �����グ/�����t���O
    public float Fountain_velocity_ { get { return fountain_velocity_; } }
    [SerializeField, Header("�X�򂪏����݂ɗh���Ԋu����")]
    private float small_vibration_time_ = 1f;
    public float Small_vibration_time_ { get { return small_vibration_time_; } }
    [SerializeField, Header("�X�򂪏����݂ɗh���p�x(deg)")]
    private float small_vibration_mag_ = 5f;
    public float Small_vibration_mag_ { get { return small_vibration_mag_;} }
    [SerializeField, Header("����̃T�C�Y���ς�闦(�����قǂ����o��)�w��")]
    private float transform_rate_ = 1.0f;
    public float TransformRate_ { get { return transform_rate_; } }
    [SerializeField, Header("����̏����T�C�Y(�����Ȃ����炢���������Ă���)")]
    private float init_size_ = 0.01f;
    private float init_box_height = 0.25f;              // iceBox(����j�̏����ʒu���擾
    public float Init_size_ { get { return init_size_; } }
    public float InitSize_ { get { return init_size_; } }
    private Vector3 max_size_ = new Vector3(0, 0, 0);       // FountainIceBase�̏����T�C�Y
    [SerializeField, Header("GeyserManager�o�^")]
    private GeyserManager manager_;
    [SerializeField, Header("�����摜�Q�[���I�u�W�F�N�g�o�^")]
    private GameObject fountain_imgObj_;
    [SerializeField, Header("�X�̑���Q�[���I�u�W�F�N�g�o�^")]
    private GameObject fountain_baseObj_;
    [SerializeField, Header("�����̍������E�������Q�[���I�u�W�F�N�g�o�^")]
    private GameObject fountain_heightObj_;
    private float height_max_;              // �����������E
    private float fountain_height_;         // �����̍���������
    private float fountain_init_height = 1.0f;  // �����̏����������擾

    private void Awake()
    {
        
    }
    private void Start()
    {
        // ----- �����l�擾 ----- //
        height_max_ = fountain_heightObj_.transform.localPosition.y;    // �����������E
        max_size_ = fountain_baseObj_.transform.localScale;             // IceBase�̏����T�C�Y�擾
        // ----- �����ݒ� ----- //
        fountain_baseObj_.transform.localScale = new Vector3(1, 1, 1) * init_size_;         // IceBase�������Ȃ����炢����������
        init_box_height = fountain_baseObj_.transform.localPosition.y;  // IceBox�̉����ʒu���擾
        fountain_baseObj_.SetActive(false);                             // IceBase�𖳌���(�X�򂪏�����܂�)
        height_max_ = fountain_heightObj_.transform.localPosition.y;    // IceBase�������摜�̍������E�擾
        fountain_heightObj_.SetActive(false);                           // IceBase�������E�������Ȃ�����
        fountain_init_height = fountain_baseObj_.GetComponent<SpriteRenderer>().size.y; // �����摜�̏����ʒu���擾
        
        
    }

    private void FixedUpdate()
    {
        if (manager_.is_Pause_) { return; }             // �|�[�Y���̓X�N���v�g����
        if (!manager_.is_IceRock_melted_) { return; }   // IceRock���n������܂ŃX�N���v�g����
        if(fountain_baseObj_.activeSelf == false) { fountain_baseObj_.SetActive(true); }    // ����̗L����
        FountainMove(height_max_, fountain_velocity_, Time.fixedDeltaTime, ref fountain_imgObj_, ref fountain_baseObj_);

    }

    private void FountainMove(float height, float velocity, float delta_time, ref GameObject fountainImg, ref GameObject fountainBaseObj)
    {
        // ----- �����̍���������̈ʒu���X�V ----- //
        float height_delta = fountain_velocity_ * delta_time;
        // �������㏸������t���O
        if (!up)
        {
            // ---- �t���O�Ǘ� ---- //
            fountain_counter_ += delta_time;
            if (fountain_counter_ > fountain_time_) { up = true; }
            // --- ����̏��� ---- //
            if (fountain_baseObj_.transform.localPosition.y >= init_box_height)
            {
                // ���~���E�܂ňړ�
                fountain_baseObj_.transform.position -= new Vector3(0, height_delta, 0);
            }
            // --- �����摜�̏��� ---- //
            if (fountain_imgObj_.transform.localPosition.y >= fountain_init_height/2)
            {
                var img = fountainImg.GetComponent<SpriteRenderer>();
                img.size -= new Vector2(0, height_delta);
                fountainImg.transform.localPosition -= new Vector3(0, height_delta / 2, 0);
            }
        }
        if (up)
        {
            // ---- �t���O�Ǘ� ---- //
            fountain_counter_ -= delta_time;
            if (fountain_counter_ < 0) { up = false; }
            // --- ����̏��� ---- //
            // �������E���������~
            if (fountain_baseObj_.transform.localPosition.y < height)
            {
                
                fountain_baseObj_.transform.position += new Vector3(0, height_delta, 0);
            }
            // --- �����摜�̏��� --- //
            // �������E���������~
            if (fountain_imgObj_.transform.localPosition.y < height/2)
            {
                var img = fountainImg.GetComponent<SpriteRenderer>();
                img.size += new Vector2(0, height_delta);
                fountainImg.transform.localPosition += new Vector3(0, height_delta / 2, 0);
            }

        }


    }
}

