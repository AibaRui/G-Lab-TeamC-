using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class AvalancheController : GimickBase
{
    [SerializeField, Tooltip("雪崩の進行速度")]
    private float _speed = 1.0f;
    /// <summary>ゲームオブジェクトにアタッチされているRigidbody2D</summary>
    private Rigidbody2D _rb;
    /// <summary>子オブジェクトのAnimator</summary>
    Animator _anim = null;

    //パラメーターを保存する用の変数
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
            Debug.Log($"You Are Dead...  「{collision.gameObject.name}」");
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
