using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(AudioSource))]

class ScaffoldController : GimickBase
{
    [SerializeField, Range(1f, 7f), Tooltip("���̏�Ԃɉ񕜂���܂ł̎���")]
    private float _interval = 1.0f;
    /// <summary>�I�[���̐ڐG���Ԃ��v������^�C�}�[</summary>
    private float _timer = 0.0f;
    /// <summary>���̃Q�[���I�u�W�F�N�g�����܂ꂽ��</summary>
    private int _stateCount = 0;
    /// <summary>�`�悷��摜</summary>
    private SpriteRenderer _mainSprite = null;
    [SerializeField, Tooltip("�f�t�H���g�̉摜")]
    private Sprite _defaultSprite = null;
    [SerializeField, Tooltip("�Ђт��������摜")]
    private Sprite _changeSprite = null;
    [SerializeField, Tooltip("���ꂽ�摜")]
    private Sprite _breakSprite = null;
    /// <summary>�Đ����鉹</summary>
    private AudioSource _mainAudio = null;
    [SerializeField, Tooltip("1�񓥂񂾎���SE")]
    private AudioClip _firstCrack = null;
    [SerializeField, Tooltip("2�񓥂񂾎���SE")]
    private AudioClip _secondCrack = null;
    [SerializeField, Tooltip("�����Ƃ���SE")]
    private AudioClip _break = null;
    /// <summary>�����蔻����t�B���^�����O����</summary>
    private ContactFilter2D _filter;
    /// <summary>�Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���BoxCollider2D</summary>
    private BoxCollider2D _boxCol2D;
    /// <summary>�Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���Rigidbody2D</summary>
    private Rigidbody2D _rb;

    private void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
        if (!_defaultSprite || !_changeSprite || !_breakSprite)
        {
            Debug.LogWarning("�ݒ肳��Ă��Ȃ��摜������܂��B");
        }

        _mainAudio = GetComponent<AudioSource>();
        if (!_firstCrack || !_secondCrack || !_break)
        {
            Debug.LogWarning("�ݒ肳��Ă��Ȃ����ʉ�������܂��B");
        }
        

        _boxCol2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _filter.layerMask = LayerMask.GetMask("Player");  //���肷��Layer��Player�ɐ�������
        _filter.useNormalAngle = true;                    //���肷��͈͂�240���` 300���ɐ�������
        _filter.SetNormalAngle(240f, 300f);
    }

    private void ScaffoldState()
    {
        switch (_stateCount)  //���񓥂܂ꂽ���ɉ����ď�����؂�ւ���
        {
            case 0:  // �������
                _mainSprite.sprite = _defaultSprite;
                break;
            case 1:  // 1��ڂɓ��܂ꂽ�Ƃ�
                _boxCol2D.isTrigger = false;
                _mainSprite.sprite = _changeSprite;
                _mainSprite.DOFade(1f, 1f);
                _mainAudio.PlayOneShot(_firstCrack);
                gameObject.layer = 11;
                break;
            case 2:  // 2��ڂɓ��܂ꂽ�Ƃ�
                _mainSprite.DOFade(0.5f, 1f);
                _mainAudio.PlayOneShot(_secondCrack);
                DOVirtual.DelayedCall(1.0f, () =>
                {
                    _boxCol2D.isTrigger = true;
                    gameObject.layer = default;
                    _mainSprite.sprite = _breakSprite;
                    _mainAudio.PlayOneShot(_break);
                }, false);
                break;
        }
    }
    //Player�����ɏ������A���܂ꂽ�񐔂𑝉�������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            if (_stateCount < 2 && _rb.IsTouching(_filter)) _stateCount++;
            ScaffoldState();
        }
        Debug.Log(_stateCount);
    }

    //��莞�ԁu�ł߂�v�I�[��������������A���܂ꂽ�񐔂�����������
    private void OnTriggerStay2D(Collider2D collision)
    {
        //�u�ł߂�v�I�[��������������A_stateCount������������
        if (collision.gameObject.tag is "Cool" && _stateCount > 0)
        {
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                _stateCount--;
                _timer = 0;
                ScaffoldState();
                Debug.Log(_stateCount);
            }
        }
    }

    public override void GameOverPause()
    {
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Pause()
    {
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Resume()
    {
        _rb.simulated = true;
        _rb.WakeUp();
    }
}
