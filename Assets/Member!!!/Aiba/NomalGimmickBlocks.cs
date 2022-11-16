using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalGimmickBlocks : MonoBehaviour
{
    [Header("�I�[���ɂ���ĕω�����܂ł̎���")]
    [SerializeField] float _timeLimit = 5f;
    [SerializeField] float _timeCount = 0;

    [SerializeField] Color _colorHot;
    [SerializeField] Color _colorCool;

    [Header("�X�̏�Ԃ̎��̃��C���[(Player���C���[�Ɠ�����)")]
    [Tooltip("�X�̏�Ԃ̎��̃��C���[(Player���C���[�Ɠ�����)")] [SerializeField] int _coolLayer;

    [Header("���̏�Ԃ̎��̃��C���[(Player���C���[�Ɠ�����Ȃ�)")]
    [Tooltip("���̏�Ԃ̎��̃��C���[(Player���C���[�Ɠ�����Ȃ�)")] [SerializeField] int _hotLayer;

    [Header("�M�̔\�͂̃^�O")]
    [SerializeField] string _hotTagName;

    [Header("��C�̔\�͂̃^�O")]
    [SerializeField] string _coolTagName;


    //���݁A�M�̃I�[���ɂ������Ă��邩�ǂ�����
    bool hot;
    //���݁A��C�̃I�[���ɂ������Ă��邩�ǂ�����
    bool cool;

    [SerializeField] BrockState _brockState = BrockState.Ice;

    void Start()
    {
        ChangeBlock();
    }

    void Update()
    {        //��C�̃I�[���������������Ă���Ƃ�
        if (!hot && cool)
        {
            //��Ԃ��M��������
            if (_brockState == BrockState.Hot)
            {
                _timeCount += Time.deltaTime;

                //��莞�ԓ������Ă������Ԃ�ω�������
                if (_timeCount >= _timeLimit)
                {
                    _brockState = BrockState.Ice;
                    _timeCount = 0;
                    ChangeBlock();
                }
            }
        }
        //�M�̃I�[���������������Ă���Ƃ�
        if (hot && !cool)
        {
            //��Ԃ��X��������
            if (_brockState == BrockState.Ice)
            {
                _timeCount += Time.deltaTime;

                //��莞�ԓ������Ă������Ԃ�ω�������
                if (_timeCount >= _timeLimit)
                {
                    _brockState = BrockState.Hot;
                    _timeCount = 0;
                    ChangeBlock();
                }
            }
        }

        //�ǂ������������Ă��Ȃ�������
        if (!hot && !cool)
        {
            //Debug.Log("no");
            //���Ԃ��ȏゾ������A���ɖ߂�
            if (_timeCount > 0)
            {
                _timeCount -= Time.deltaTime;
                if (_timeCount < 0)
                {
                    _timeCount = 0;
                }
            }
        }
    }

    //State�̏�Ԃɉ����āA�`���ς���
    void ChangeBlock()
    {
        if (_brockState == BrockState.Hot)
        {
            this.GetComponent<SpriteRenderer>().color = _colorHot;
            gameObject.layer = _hotLayer;
            
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = _colorCool;
            gameObject.layer = _coolLayer;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _coolTagName)
        {
            cool = true;
        }

        if (collision.gameObject.tag == _hotTagName)
        {
            hot = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _coolTagName)
        {
            cool = false;
        }

        if (collision.gameObject.tag == _hotTagName)
        {
            hot = false;
        }
    }


    enum BrockState
    {
        Ice,
        Hot,
    }


}
