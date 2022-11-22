using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]

class ScaffoldController : GimickBase
{
    [SerializeField, Range(1f, 7f), Tooltip("���̏�Ԃɉ񕜂���܂ł̎���")]
    private float _interval = 1.0f;
    /// <summary>�I�[���̐ڐG���Ԃ��v������^�C�}�[</summary>
    private float _timer = 0.0f;
    /// <summary>���̃Q�[���I�u�W�F�N�g�����܂ꂽ��</summary>
    private int _stateCount = 0;
    /// <summary>�`�悷��Sprite</summary>
    private SpriteRenderer _mainSprite;
    /// <summary>�����蔻����t�B���^�����O����</summary>
    private ContactFilter2D _filter;
    private BoxCollider2D _boxCol2D;
    private Rigidbody2D _rb;

    private void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
        _boxCol2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _filter.layerMask = LayerMask.GetMask("Player");  //���肷��Layer��Player�ɐ�������
        _filter.useNormalAngle = true;                    //���肷��͈͂�240���` 300���ɐ�������
        _filter.SetNormalAngle(240f, 300f);
    }

    private void FixedUpdate()
    {
        switch (_stateCount)  //��Ԃɉ����ď�����؂�ւ���
        {
            case 0:
                _mainSprite.color = Color.red;
                break;
            case 1:
                _boxCol2D.isTrigger = false;
                _mainSprite.color = Color.blue;
                gameObject.layer = 11;
                break;
            case 2:
                _mainSprite.color = Color.green;
                DOVirtual.DelayedCall(1.0f, () =>
                {
                    _boxCol2D.isTrigger = true;
                    gameObject.layer = default;
                }, false);
                break;
        }
    }

    //Player�����ɏ������A���܂ꂽ�񐔂𑝉�������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            if (_stateCount < 2 && _rb.IsTouching(_filter)) _stateCount++;
        }
        Debug.Log(_stateCount);
    }

    //��莞�ԁu�ł߂�v�I�[��������������A���܂ꂽ�񐔂�����������
    private void OnTriggerStay2D(Collider2D collision)
    {
        //�u�ł߂�v�I�[��������������A_stateCount������������
        if (collision.gameObject.tag == "Cool" && _stateCount >= 1)
        {
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                _stateCount--;
                _timer = 0;
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
