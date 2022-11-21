using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : GimickBase
{ 
    [SerializeField, Header("EnemyManager�o�^")]
    private EnemyManager Em_;
    [SerializeField, Header("���G�͈�(�~)�R���C�_�o�^")]
    private CircleCollider2D search_area_;
    [SerializeField, Header("���G�̗L��")]
    private bool is_target_ = false;
    public bool Is_target_ { get { return is_target_; } set { is_target_ = value; } }
    [SerializeField, Header("�ŏ��ɔ��������^�[�Q�b�g��Transform���i�[����")]
    private Transform target_transform_;
    public Transform Target_trans_ { get { return target_transform_; } }

    private bool is_Pause_ = false;
    private List<string>target_tag_;
   

    private void Start()
    {
        // ----- ������ ----- //
        // ---- ���G�Ώێ擾 ---- //
        target_tag_ = Em_.Target_tag_;
    }

    private void FixedUpdate()
    {
        Debug.Log(target_transform_.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ---- ���G�Ώۂ��͈͓��ɓ��������F�^�[�Q�b�g�����t���O�L�� ----- //
        // --- ���G������(is_target = true)�A�O������t���O��܂�Ȃ�����č��G���Ȃ� --- //
        // --- �|�[�Y�������얳�� --- //
        if (is_Pause_) { return; }
        if (is_target_) { return; }
        for (var i = 0; i < target_tag_.Count; i++)
        {
            if (other.CompareTag(target_tag_[i]))
            {
                // --- �^�[�Q�b�g��Transform�Q�ƒl�擾 --- //
                is_target_ = true;
                target_transform_ = other.transform;
            
            }
        }
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
