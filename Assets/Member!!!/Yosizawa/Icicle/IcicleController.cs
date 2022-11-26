using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class IcicleController : GimickBase
{
    [SerializeField, Tooltip("�������x")]
    private float _fallSpeed = 1.0f;
    [SerializeField, Range(1f, 10f), Tooltip("�I�[���ڐG���̊g��E�k���{��")]
    private float _magnification = 1.0f;
    [SerializeField, Tooltip("�Q�[���I�u�W�F�N�g�̑傫���̍ŏ�")]
    private float _minSize = 2.0f;
    [SerializeField, Tooltip("�Q�[���I�u�W�F�N�g�̑傫���̍ő�")]
    private float _maxSize = 6.0f;
    [SerializeField, Tooltip("Ray�̕\���E��\���̐؂�ւ�")]
    private bool _isGizmo = false;
    [SerializeField, Range(-5f, 5f), Tooltip("Ray���΂�����")]
    private float _direction = 1f;
    /// <summary>Ray�̋���</summary>
    private float _length = 50f;
    /// <summary>Ray���΂�����</summary>
    private Vector2 _dir = Vector2.zero;
    /// <summary>Ray���΂��ē�������collider�̏��</summary>
    private RaycastHit2D _hit;
    /// <summary>���̑傫���ɂȂ������𔻒肷��t���O</summary>
    private bool _isScale =false;
    /// <summary>Rigidbody2D�^�̕ϐ�</summary>
    private Rigidbody2D _rb;
    //�p�����[�^�[�̏���ۑ�����p�̕ϐ�
    private float _saveGravityScale;
    private float _saveAngularVelocity;
    private Vector2 _saveVelocity;

    private void Start()
    {
        _magnification /= 100f;  //�������Ɣ{������������̂ŁA���炩���ߒႭ���Ă���
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        //Ray�ɓ�������collider��Player���ǂ������肷��
        _hit = Physics2D.Raycast(transform.position, _dir, _length, LayerMask.GetMask("Player"));

        if(_hit.collider)  //Ray��Player�ɓ����������̏���
        {
            _rb.gravityScale = _fallSpeed;
        }
    }

    /// <summary>Ray���������邽�߂̊֐�</summary>
    private void OnDrawGizmos()
    {
        //isGizmo �� treu �ɂȂ��Ă����� Gizmo ���\���ɂ���
        if (_isGizmo is false) return;

        //Ray���΂��������������Ɍ��肷��
        _dir = new Vector2(_direction, -1).normalized;
        Gizmos.DrawRay(transform.position, _dir * _length);
    }

    private void OnTriggerStay2D(Collider2D collision)
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

            //�Q�[���I�u�W�F�N�g���ő�T�C�Y��������A
            if(transform.localScale.x >= _maxSize && transform.localScale.y >= _maxSize)
            {
                _isScale = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag is "Ground")
        {
            //�Q�[���I�u�W�F�N�g�� scale ���ő�T�C�Y��������A�������Ɏc��
            if(_isScale)
            {
                _rb.constraints = 
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                Debug.Log("Alien");
            }

            //�Q�[���I�u�W�F�N�g�� scale ���ő�T�C�Y�łȂ�������A������
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public override void GameOverPause()
    {
        _saveGravityScale = _rb.gravityScale;
        _saveAngularVelocity = _rb.angularVelocity;
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Pause()
    {
        _saveGravityScale = _rb.gravityScale;
        _saveAngularVelocity = _rb.angularVelocity;
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Resume()
    {
        _rb.simulated = true;
        _rb.WakeUp();
        _rb.gravityScale = _saveGravityScale;
        _rb.angularVelocity = _saveAngularVelocity;
        _rb.velocity = _saveVelocity;
    }
}