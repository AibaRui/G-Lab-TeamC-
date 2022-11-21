using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class IcicleController : GimickBase
{
    [Header("�������x")]
    [SerializeField] float _fallSpeed = 1.0f;
    [Header("�I�[���ڐG���̊g��E�k���{��")]
    [SerializeField,Range(1f, 10f),Tooltip("�Q�[���I�u�W�F�N�g �� scale �ω��{��")] float _magnification = 1.0f;
    [Header("���̃Q�[���I�u�W�F�N�g�̍ŏ��̑傫��")]
    [SerializeField,Tooltip("�Q�[���I�u�W�F�N�g�� scale �̍ŏ�")] float _minSize = 2.0f;
    [Header("���̃Q�[���I�u�W�F�N�g�̍ő�̑傫��")]
    [SerializeField, Tooltip("�Q�[���I�u�W�F�N�g�� scale �̍ő�")] float _maxSize = 6.0f;
    [Header("Player�����m��������� �\�� or ��\��")]
    [SerializeField,Tooltip("Ray�̕\���E��\���̐؂�ւ�")] bool _isGizmo = false;
    [Header("Player�����m����������΂�����")]
    [SerializeField,Range(-5f, 5f),Tooltip("Ray���΂�����")] float _direction = 1f;
    /// <summary>Ray�̋���</summary>
    float _length = 50f;
    /// <summary>Ray���΂�����</summary>
    Vector2 _dir = Vector2.zero;
    /// <summary>Ray���΂��ē�������collider�̏��</summary>
    RaycastHit2D _hit;
    /// <summary>���̑傫���ɂȂ������𔻒肷��t���O</summary>
    bool _isScale =false;
    Rigidbody2D _rb1;
    float _saveGravityScale;
    float _saveAngularVelocity;
    Vector2 _saveVelocity;

    void Start()
    {
        _magnification /= 100f;  //�������Ɣ{������������̂ŁA���炩���ߒႭ���Ă���
        _rb1 = GetComponent<Rigidbody2D>();
        _rb1.gravityScale = 0;
    }

    void Update()
    {
        //Ray�ɓ�������collider��Player���ǂ������肷��
        _hit = Physics2D.Raycast(transform.position, _dir, _length, LayerMask.GetMask("Player"));

        if(_hit.collider)  //Ray��Player�ɓ����������̏���
        {
            _rb1.gravityScale = _fallSpeed;
        }
    }

    /// <summary>Ray���������邽�߂̊֐�</summary>
    void OnDrawGizmos()
    {
        if (_isGizmo == false) return;

        _dir = new Vector2(_direction, -1).normalized;
        Gizmos.DrawRay(transform.position, _dir * _length);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //�Q�[���I�u�W�F�N�g�̑傫���̏�񂪓������ϐ�
        var localScale = transform.localScale;

        //�Q�[���I�u�W�F�N�g���ɒ[�ɑ傫��(������)�Ȃ�Ȃ��悤���Ă���
        localScale.x = Mathf.Clamp(localScale.x, _minSize, _maxSize);
        localScale.y = Mathf.Clamp(localScale.y, _minSize, _maxSize);

        //���������I�[�����u�n�����v�Ȃ�Q�[���I�u�W�F�N�g�̑傫��������
        if (collision.gameObject.tag is "Hot")
        {
            localScale.x -= _magnification;
            localScale.y -= _magnification;
            transform.localScale = localScale;
        }
        //���������I�[�����u�ł߂�v�Ȃ�Q�[���I�u�W�F�N�g�̑傫���𑝉�
        if (collision.gameObject.tag is "Cool")
        {
            localScale.x += _magnification;
            localScale.y += _magnification;
            transform.localScale = localScale;

            if(transform.localScale.x >= _maxSize && transform.localScale.y >= _maxSize)
            {
                _isScale = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if(_isScale)
            {
                _rb1.constraints = RigidbodyConstraints2D.FreezeAll;
                //_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                Debug.Log("Alien");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public override void GameOverPause()
    {
        _saveGravityScale = _rb1.gravityScale;
        _saveAngularVelocity = _rb1.angularVelocity;
        _saveVelocity = _rb1.velocity;
        _rb1.Sleep();
        _rb1.simulated = false;
    }

    public override void Pause()
    {
        _saveGravityScale = _rb1.gravityScale;
        _saveAngularVelocity = _rb1.angularVelocity;
        _saveVelocity = _rb1.velocity;
        _rb1.Sleep();
        _rb1.simulated = false;
    }

    public override void Resume()
    {
        _rb1.simulated = true;
        _rb1.WakeUp();
        _rb1.gravityScale = _saveGravityScale;
        _rb1.angularVelocity = _saveAngularVelocity;
        _rb1.velocity = _saveVelocity;
    }
}