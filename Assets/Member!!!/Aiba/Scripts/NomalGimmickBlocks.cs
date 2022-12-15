using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalGimmickBlocks : GimickBase
{
    [Header("�I�[���ɂ���ĕω�����܂ł̎���")]
    [SerializeField] float _timeLimit = 5f;

    private float _timeCountCool = 0;
    private float _timeCountHot = 0;


    [Header("�n���Ă鎞�̃C���X�g")]
    [SerializeField] Sprite _spriteWater;

    [Header("�ł܂��Ă鎞�̃C���X�g")]
    [SerializeField] Sprite _spriteSnow;

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

    [Header("�����̏�Ԃ����߂�")]
    [SerializeField] BrockState _brockState = BrockState.Ice;

    [Header("�n�������̉�")]
    [SerializeField] AudioClip _hotSound;
    [Header("�ł܂������̉�")]
    [SerializeField] AudioClip _coolSound;
    bool _isPause = false;

    AudioSource _aud;
    SpriteRenderer _sprite;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _aud = GetComponent<AudioSource>();
        ChangeBlock();
    }

    void Update()
    {
        if (!_isPause)
        {
            //��C�̃I�[���������������Ă���Ƃ�
            if (!hot && cool)
            {
                //��Ԃ��M��������
                if (_brockState == BrockState.Hot)
                {
                    _timeCountCool += Time.deltaTime;

                    //��莞�ԓ������Ă������Ԃ�ω�������
                    if (_timeCountCool >= _timeLimit)
                    {
                        _brockState = BrockState.Ice;
                        _timeCountCool = 0;
                        ChangeBlock();
                        _aud.PlayOneShot(_coolSound);
                    }
                }
            }
            //�M�̃I�[���������������Ă���Ƃ�
            if (hot && !cool)
            {
                //��Ԃ��X��������
                if (_brockState == BrockState.Ice)
                {
                    _timeCountHot += Time.deltaTime;

                    //��莞�ԓ������Ă������Ԃ�ω�������
                    if (_timeCountHot >= _timeLimit)
                    {
                        _brockState = BrockState.Hot;
                        _timeCountHot = 0;
                        ChangeBlock();
                        _aud.PlayOneShot(_hotSound);
                    }
                }
            }

            //�ǂ������������Ă��Ȃ�������
            if (!hot && !cool)
            {
                //Debug.Log("no");
                //���Ԃ��ȏゾ������A���ɖ߂�
                if (_timeCountCool > 0)
                {
                    _timeCountCool -= Time.deltaTime;
                    if (_timeCountCool < 0)
                    {
                        _timeCountCool = 0;
                    }
                }
                if (_timeCountHot > 0)
                {
                    _timeCountHot -= Time.deltaTime;
                    if (_timeCountHot < 0)
                    {
                        _timeCountHot = 0;
                    }
                }
            }
        }

        Debug.Log(_timeCountCool);
    }

    //State�̏�Ԃɉ����āA�`���ς���
    void ChangeBlock()
    {
        if (_brockState == BrockState.Hot)
        {
            _sprite.sprite = _spriteWater;
            gameObject.layer = _hotLayer;

        }
        else
        {
            _sprite.sprite = _spriteSnow;
            gameObject.layer = _coolLayer;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isPause)
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



    /// <summary>�Q�[���I�[�o�[���ɌĂԁB�A�j���[�V�����̒�~�ARigidbody�̒�~�A�������</summary>
    public override void GameOverPause()
    {
        _isPause = true;
    }

    /// <summary>�ꎞ��~���ɌĂԁB�A�j���[�V�����̒�~�ARigidbody�̒�~�A��������̏���������</summary>
    public override void Pause()
    {
        _isPause = true;
    }
    /// <summary>�Q�[���ĊJ���ɌĂԁB�A�j���[�V�����̍ĊJ�ARigidbody�̍ĊJ�A����ĊJ������</summary>
    public override void Resume()
    {
        _isPause = false;
    }


}
