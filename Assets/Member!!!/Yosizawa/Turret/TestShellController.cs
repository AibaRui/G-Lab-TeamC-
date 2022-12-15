using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]

class TestShellController : GimickBase
{
    [SerializeField, Tooltip("弾が消失するまでの時間")]
    private float _lifeTime = 3f;
    /// <summary>経過時間を測定するタイマー</summary>
    private float _timer = 0;
    [SerializeField, Tooltip("Playerに着弾した時に加える力")]
    private float _forcePower = 1f;
    /// <summary>弾にアタッチされているコライダー</summary>
    private CircleCollider2D _col;
    /// <summary>現在、Pause中かどうかを判定するフラグ</summary>
    private bool _isPause = false;
    /// <summary>ゲームオブジェクトにアタッチされているRigidbody2D</summary>
    private Rigidbody2D _rb;
    /// <summary>砲台のゲームオブジェクト</summary>
    GameObject _turret = null;

    //パラメーターを保存する用の変数
    private float _saveAngularVelocity;
    private Vector2 _saveVelocity;

    private void Start()
    {
        _col = GetComponent<CircleCollider2D>();
        _col.isTrigger = true;
        _rb = GetComponent<Rigidbody2D>();
        _turret = FindObjectOfType<TurretController>().gameObject;
    }

    private void Update()
    {
        if(!_isPause)  //
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
            GameObject player = collision.gameObject;

            if (player.transform.position.x <= _turret.transform.position.x)
            {
                player.GetComponent<IDamagable>().AddDamage(transform, _forcePower);
                Debug.Log("Go! Left!");
            }
            else
            {
                player.GetComponent<IDamagable>().AddDamage(transform, -_forcePower);
                Debug.Log("Go! Right!");
            }
            Destroy(gameObject);
        }
    }

    public override void GameOverPause()
    {
        _isPause = true;
        _saveAngularVelocity = _rb.angularVelocity;
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Pause()
    {
        _isPause = true;
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
        _isPause = false;
    }
}
