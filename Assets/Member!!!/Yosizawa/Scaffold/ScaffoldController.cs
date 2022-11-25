using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]

class ScaffoldController : GimickBase
{
    [SerializeField, Range(1f, 7f), Tooltip("元の状態に回復するまでの時間")]
    private float _interval = 1.0f;
    /// <summary>オーラの接触時間を計測するタイマー</summary>
    private float _timer = 0.0f;
    /// <summary>このゲームオブジェクトが踏まれた回数</summary>
    private int _stateCount = 0;
    /// <summary>描画するSprite</summary>
    private SpriteRenderer _mainSprite;
    /// <summary>当たり判定をフィルタリングする</summary>
    private ContactFilter2D _filter;
    private BoxCollider2D _boxCol2D;
    private Rigidbody2D _rb;

    private void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
        _boxCol2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _filter.layerMask = LayerMask.GetMask("Player");  //判定するLayerをPlayerに制限する
        _filter.useNormalAngle = true;                    //判定する範囲を240°～ 300°に制限する
        _filter.SetNormalAngle(240f, 300f);
    }

    private void FixedUpdate()
    {
        switch (_stateCount)  //状態に応じて処理を切り替える
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

    //Playerが床に乗ったら、踏まれた回数を増加させる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            if (_stateCount < 2 && _rb.IsTouching(_filter)) _stateCount++;
        }
        Debug.Log(_stateCount);
    }

    //一定時間「固める」オーラが当たったら、踏まれた回数を減少させる
    private void OnTriggerStay2D(Collider2D collision)
    {
        //「固める」オーラが当たったら、_stateCountを減少させる
        if (collision.gameObject.tag is "Cool" && _stateCount >= 1)
        {
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                _stateCount--;
                _timer = 0;
                Debug.Log(_stateCount);
            }
        }
    }

    public override void GameOverPause()
    {
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Pause()
    {
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Resume()
    {
        _rb.simulated = true;
        _rb.WakeUp();
    }
}
