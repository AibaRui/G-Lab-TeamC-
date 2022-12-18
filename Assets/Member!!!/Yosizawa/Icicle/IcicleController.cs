using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D),typeof(AudioSource))]
class IcicleController : GimickBase
{
    [SerializeField, Tooltip("落下速度")]
    private float _fallSpeed = 1.0f;
    [SerializeField, Tooltip("氷柱がPlayerに衝突した時のPlayerの硬直時間")]
    private float _stunTime = 1f;
    [SerializeField, Tooltip("オーラ接触時の拡大・縮小倍率")]
    private float _magnification = 1.0f;
    /// <summary>ゲームオブジェクトの大きさの最小</summary>
    private float _minSize = 2.0f;
    [SerializeField, Tooltip("ゲームオブジェクトの大きさの最大")]
    private float _maxSize = 6.0f;
    [SerializeField, Tooltip("Rayの表示・非表示の切り替え")]
    private bool _isGizmo = true;
    [SerializeField, Range(-5f, 5f), Tooltip("Rayを飛ばす方向")]
    private float _direction = 1f;
    /// <summary>Rayの距離</summary>
    private float _length = 50f;
    /// <summary>Rayを飛ばす向き</summary>
    private Vector2 _dir1 = Vector2.zero;
    /// <summary>Rayを飛ばす向き</summary>
    private Vector2 _dir2 = Vector2.zero;
    /// <summary>Rayを飛ばして当たったcolliderの情報</summary>
    private RaycastHit2D _hit1;
    /// <summary>Rayを飛ばして当たったcolliderの情報</summary>
    private RaycastHit2D _hit2;
    /// <summary>一定の大きさになったかを判定するフラグ</summary>
    private bool _isScale =false;
    /// <summary>ゲームオブジェクトのRigidbody2D</summary>
    private Rigidbody2D _rb = null;
    /// <summary>ゲームオブジェクトのcollider2D</summary>
    private Collider2D _col = null;
    [SerializeField, Tooltip("氷柱が割れた時のSE")]
    private AudioClip _break = null;
    [SerializeField, Tooltip("氷柱が突き刺さった時のSE")]
    private AudioClip _stabbed = null;

    //パラメーターの情報を保存する用の変数
    private float _saveGravityScale;
    private float _saveAngularVelocity;
    private Vector2 _saveVelocity;

    private void Start()
    {
        _magnification /= 100f;  //整数だと倍率が高すぎるので、あらかじめ低くしておく
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _col = GetComponent<Collider2D>();
        _col.isTrigger = true;
    }

    private void Update()
    {
        //Rayを飛ばす方向を下方向に限定する
        _dir1 = new Vector2(_direction, -1).normalized;
        _dir2 = new Vector2(-_direction, -1).normalized;

        //Rayに当たったcolliderがPlayerかどうか判定する
        _hit1 = Physics2D.Raycast(transform.position, _dir1, _length, LayerMask.GetMask("Player"));
        _hit2 = Physics2D.Raycast(transform.position, _dir2, _length, LayerMask.GetMask("Player"));

        if (_hit1.collider || _hit2.collider)  //RayがPlayerに当たった時の処理
        {
            _rb.gravityScale = _fallSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag is "Hot" && !_isScale)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.simulated = false;
            DOVirtual.DelayedCall(_stunTime, () =>
            {
                playerRb.simulated = true;
                //Destroy(gameObject);
            }, false);
        }
        if (collision.gameObject.tag is "Ground")
        {
            //ゲームオブジェクトの scale が最大サイズだったら、消えずに残る
            if (_isScale)
            {
                _col.isTrigger = false;
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = _stabbed;
                audio.Play();
                //AudioSource.PlayClipAtPoint(_stabbed, transform.position);
                _rb.constraints =
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            //ゲームオブジェクトの scale が最大サイズでなかったら、消える
            else
            {
                AudioSource.PlayClipAtPoint(_break, transform.position);
                Destroy(gameObject);
            }
        }
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
            Debug.Log(localScale);
            if (localScale.x <= _minSize && localScale.y <= _minSize)
            {
                AudioSource.PlayClipAtPoint(_break, transform.position);
                Destroy(gameObject);
            }
        }
        //当たったオーラが「固める」ならゲームオブジェクトの大きさを増加
        if (collision.gameObject.tag is "Cool")
        {
            localScale.x += _magnification;
            localScale.y += _magnification;
            transform.localScale = localScale;

            //ゲームオブジェクトが最大サイズだったら、
            if(transform.localScale.x >= _maxSize && transform.localScale.y >= _maxSize)
            {
                _isScale = true;
            }
        }
    }

    /// <summary>Rayを可視化するための関数</summary>
    private void OnDrawGizmos()
    {
        //isGizmo が false になっていたら Gizmo を非表示にする
        if (_isGizmo is false) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _dir1 * _length);
        Gizmos.DrawRay(transform.position, _dir2 * _length);

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