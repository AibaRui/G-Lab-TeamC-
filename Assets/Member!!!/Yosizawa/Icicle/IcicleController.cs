using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class IcicleController : GimickBase
{
    [SerializeField, Tooltip("落下速度")]
    private float _fallSpeed = 1.0f;
    [SerializeField, Range(1f, 10f), Tooltip("オーラ接触時の拡大・縮小倍率")]
    private float _magnification = 1.0f;
    [SerializeField, Tooltip("ゲームオブジェクトの大きさの最小")]
    private float _minSize = 2.0f;
    [SerializeField, Tooltip("ゲームオブジェクトの大きさの最大")]
    private float _maxSize = 6.0f;
    [SerializeField, Tooltip("Rayの表示・非表示の切り替え")]
    private bool _isGizmo = false;
    [SerializeField, Range(-5f, 5f), Tooltip("Rayを飛ばす方向")]
    private float _direction = 1f;
    /// <summary>Rayの距離</summary>
    private float _length = 50f;
    /// <summary>Rayを飛ばす向き</summary>
    private Vector2 _dir = Vector2.zero;
    /// <summary>Rayを飛ばして当たったcolliderの情報</summary>
    private RaycastHit2D _hit;
    /// <summary>一定の大きさになったかを判定するフラグ</summary>
    private bool _isScale =false;
    private Rigidbody2D _rb;
    private float _saveGravityScale;
    private float _saveAngularVelocity;
    private Vector2 _saveVelocity;

    private void Start()
    {
        _magnification /= 100f;  //整数だと倍率が高すぎるので、あらかじめ低くしておく
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        //Rayに当たったcolliderがPlayerかどうか判定する
        _hit = Physics2D.Raycast(transform.position, _dir, _length, LayerMask.GetMask("Player"));

        if(_hit.collider)  //RayがPlayerに当たった時の処理
        {
            _rb.gravityScale = _fallSpeed;
        }
    }

    /// <summary>Rayを可視化するための関数</summary>
    private void OnDrawGizmos()
    {
        if (_isGizmo == false) return;

        _dir = new Vector2(_direction, -1).normalized;
        Gizmos.DrawRay(transform.position, _dir * _length);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //ゲームオブジェクトの大きさの情報が入った変数
        var localScale = transform.localScale;

        //ゲームオブジェクトが極端に大きく(小さく)ならないようしておく
        localScale.x = Mathf.Clamp(localScale.x, _minSize, _maxSize);
        localScale.y = Mathf.Clamp(localScale.y, _minSize, _maxSize);

        //当たったオーラが「溶かす」ならゲームオブジェクトの大きさを減少
        if (collision.gameObject.tag is "Hot")
        {
            localScale.x -= _magnification;
            localScale.y -= _magnification;
            transform.localScale = localScale;
        }
        //当たったオーラが「固める」ならゲームオブジェクトの大きさを増加
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if(_isScale)
            {
                _rb.constraints = 
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
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