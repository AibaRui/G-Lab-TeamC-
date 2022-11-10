using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    [SerializeField, Header("GeyserManager�o�^")]
    private GeyserManager manager_;
    [SerializeField, Header("�����摜�Q�[���I�u�W�F�N�g�o�^")]
    private GameObject fountain_imgObj_;
    [SerializeField, Header("�X�̑���Q�[���I�u�W�F�N�g�o�^")]
    private GameObject fountain_baseObj_;
    [SerializeField, Header("�����̍������E�������Q�[���I�u�W�F�N�g�o�^")]
    private GameObject fountain_heightObj_;
    private float height_max_;              // �����������E
    [SerializeField, Header("�����݂ɗh���Ԋu����")]
    private float small_vibration_time_ = 1f;
    public float Small_vibration_time_ { get { return small_vibration_time_; } }
    [SerializeField, Header("�����݂ɗh���p�x(deg)")]
    private float small_vibration_mag_ = 5f;
    public float Small_vibration_mag_ { get { return small_vibration_mag_;} }
    [SerializeField, Header("IceRock�̃T�C�Y���ς�闦�w��")]
    private float transform_rate_ = 1.0f;
    public float TransformRate_ { get { return transform_rate_; } }
    [SerializeField, Header("IceBase�̏����T�C�Y")]
    private float init_size_ = 0.01f;
    public float Init_size_ { get { return init_size_; } }
    public float InitSize_ { get { return init_size_; } }
    private Vector3 max_size_ = new Vector3(0, 0, 0);       // FountainIceBase�̏����T�C�Y

    private void Start()
    {
        // ----- �����l�擾 ----- //
        height_max_ = fountain_heightObj_.transform.localPosition.y;    // �����������E
        max_size_ = fountain_baseObj_.transform.localScale;             // IceBase�̏����T�C�Y�擾
        // ----- �����ݒ� ----- //
        fountain_baseObj_.transform.localScale = new Vector3(1, 1, 1) * init_size_;     // IceBase�������Ȃ����炢����������
        fountain_heightObj_.SetActive(false);                           // IceBase�������E�������Ȃ�����

    }

    private void FixedUpdate()
    {
        if (manager_.is_Pause_) { return; }             // �|�[�Y���̓X�N���v�g����
        if (!manager_.is_IceRock_melted_) { return; }   // IceRock���n������܂ŃX�N���v�g����

    }

  

}

