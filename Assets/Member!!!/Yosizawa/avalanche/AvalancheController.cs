using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class AvalancheController : GimickBase
{
    [SerializeField, Tooltip("����̐i�s���x")]
    private float _speed = 1.0f;
    /// <summary>�Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���Rigidbody2D</summary>
    private Rigidbody2D _rb;
    /// <summary>�q�I�u�W�F�N�g��Animator</summary>
    Animator _anim = null;

    //�p�����[�^�[��ۑ�����p�̕ϐ�
    private Vector2 _saveVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _rb.velocity = Vector2.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag is "Player1" or "Player2")
        {
            Debug.Log($"You Are Dead...  �u{collision.gameObject.name}�v");
        }
    }

    public override void GameOverPause()
    {
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
        _anim.speed = 0;
    }

    public override void Pause()
    {
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
        _anim.speed = 0;
    }

    public override void Resume()
    {
        _anim.speed = 1;
        _rb.simulated = true;
        _rb.WakeUp();
        _rb.velocity = _saveVelocity;
    }
}
