using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]

class TestShellController : GimickBase
{
    [Tooltip("�e����������܂ł̎���")]
    private float _lifeTime = 3f;
    /// <summary>�o�ߎ��Ԃ𑪒肷��^�C�}�[</summary>
    private float _timer = 0;
    /// <summary>�e�ɃA�^�b�`����Ă���R���C�_�[</summary>
    private CircleCollider2D _col;
    /// <summary>���݁APause�����ǂ����𔻒肷��t���O</summary>
    private bool _isPause = true;
    /// <summary>�Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���Rigidbody2D</summary>
    private Rigidbody2D _rb;

    //�p�����[�^�[��ۑ�����p�̕ϐ�
    private float _saveAngularVelocity;
    private Vector2 _saveVelocity;

    private void Start()
    {
        _col = GetComponent<CircleCollider2D>();
        _col.isTrigger = true;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_isPause)
        {
            _timer = Time.deltaTime;
            if(_timer >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag is "Player1" or "Player2")
        {
            Debug.Log("HIT");
            Destroy(gameObject);
        }
    }

    public override void GameOverPause()
    {
        _saveAngularVelocity = _rb.angularVelocity;
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Pause()
    {
        _saveAngularVelocity = _rb.angularVelocity;
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Resume()
    {
        _rb.simulated = true;
        _rb.WakeUp();
        _rb.angularVelocity = _saveAngularVelocity;
        _rb.velocity = _saveVelocity;
    }
}
