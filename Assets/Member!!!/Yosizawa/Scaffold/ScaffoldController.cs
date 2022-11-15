using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]

public class ScaffoldController : MonoBehaviour
{
    [Header("���̏�Ԃɉ񕜂���܂ł̎���")]
    [SerializeField, Range(1f, 7f), Tooltip("���̏�Ԃɉ񕜂���܂ł̎���")] float _interval = 1.0f;
    //[Header("�f�t�H���g�̃C���X�g")]
    //[SerializeField] SpriteRenderer _firstSprite = default;
    //[Header("1�x���܂ꂽ��Ԃ̉摜")]
    //[SerializeField] SpriteRenderer _secondSprite = default;
    float _time = 0.0f;
    /// <summary>���̃Q�[���I�u�W�F�N�g�����܂ꂽ��</summary>
    int _stateCount = 0;
    /// <summary>�`�悷��Sprite</summary>
    SpriteRenderer _mainSprite;
    /// <summary>�����蔻����t�B���^�����O����</summary>
    ContactFilter2D _filter;
    BoxCollider2D _boxCol2D;
    Rigidbody2D _rb;

    void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
        _boxCol2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _filter.layerMask = LayerMask.GetMask("Player");  //���肷��Layer��Player�ɐ�������
        _filter.useNormalAngle = true;                    //���肷��͈͂�240���` 300���ɐ�������
        _filter.SetNormalAngle(240f, 300f);
    }

    void Update()
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

    //Player�����ɏ������A_stateCount�𑝉�������
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            if (_stateCount < 2 && _rb.IsTouching(_filter)) _stateCount++;
        }
        Debug.Log(_stateCount);
    }

    //�u�ł߂�v�I�[��������������A_stateCount������������
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cool" && _stateCount >= 1)
        {
            _time += Time.deltaTime;
            if (_time >= _interval)
            {
                _stateCount--;
                _time = 0;
                Debug.Log(_stateCount);
            }
        }
    }
}
