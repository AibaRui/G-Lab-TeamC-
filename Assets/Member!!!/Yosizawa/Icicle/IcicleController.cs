using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class IcicleController : GimickBase
{
    [Header("落下速度")]
    [SerializeField] float _fallSpeed = 1.0f;
    [Header("オーラ接触時の拡大・縮小倍率")]
    [SerializeField,Range(1f, 10f),Tooltip("ゲームオブジェクト の scale 変化倍率")] float _magnification = 1.0f;
    [Header("このゲームオブジェクトの最小の大きさ")]
    [SerializeField,Tooltip("ゲームオブジェクトの scale の最小")] float _minSize = 2.0f;
    [Header("このゲームオブジェクトの最大の大きさ")]
    [SerializeField, Tooltip("ゲームオブジェクトの scale の最大")] float _maxSize = 6.0f;
    [Header("Playerを検知する光線を 表示 or 非表示")]
    [SerializeField,Tooltip("Rayの表示・非表示の切り替え")] bool _isGizmo = false;
    [Header("Playerを検知する光線を飛ばす方向")]
    [SerializeField,Range(-5f, 5f),Tooltip("Rayを飛ばす方向")] float _direction = 1f;
    /// <summary>Rayの距離</summary>
    float _length = 50f;
    /// <summary>Rayを飛ばす向き</summary>
    Vector2 _dir = Vector2.zero;
    /// <summary>Rayを飛ばして当たったcolliderの情報</summary>
    RaycastHit2D _hit;
    /// <summary>一定の大きさになったかを判定するフラグ</summary>
    bool _isScale =false;
    Rigidbody2D _rb1;
    float _saveGravityScale;
    float _saveAngularVelocity;
    Vector2 _saveVelocity;

    void Start()
    {
        _magnification /= 100f;  //整数だと倍率が高すぎるので、あらかじめ低くしておく
        _rb1 = GetComponent<Rigidbody2D>();
        _rb1.gravityScale = 0;
    }

    void Update()
    {
        //Rayに当たったcolliderがPlayerかどうか判定する
        _hit = Physics2D.Raycast(transform.position, _dir, _length, LayerMask.GetMask("Player"));

        if(_hit.collider)  //RayがPlayerに当たった時の処理
        {
            _rb1.gravityScale = _fallSpeed;
        }
    }

    /// <summary>Rayを可視化するための関数</summary>
    void OnDrawGizmos()
    {
        if (_isGizmo == false) return;

        _dir = new Vector2(_direction, -1).normalized;
        Gizmos.DrawRay(transform.position, _dir * _length);
    }

    void OnTriggerStay2D(Collider2D collision)
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