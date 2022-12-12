using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(AudioSource))]

class ScaffoldController : GimickBase
{
    [SerializeField, Range(1f, 7f), Tooltip("元の状態に回復するまでの時間")]
    private float _interval = 1.0f;
    /// <summary>オーラの接触時間を計測するタイマー</summary>
    private float _timer = 0.0f;
    /// <summary>このゲームオブジェクトが踏まれた回数</summary>
    private int _stateCount = 0;
    /// <summary>描画する画像</summary>
    private SpriteRenderer _mainSprite = null;
    [SerializeField, Tooltip("デフォルトの画像")]
    private SpriteRenderer _defaultSprite = null;
    [SerializeField, Tooltip("切り替える画像")]
    private SpriteRenderer _changeSprite = null;
    /// <summary>再生する音</summary>
    private AudioSource _mainAudio = null;
    [SerializeField, Tooltip("1回踏んだ時のSE")]
    private AudioClip _firstCrack = null;
    [SerializeField, Tooltip("2回踏んだ時のSE")]
    private AudioClip _secondCrack = null;
    [SerializeField, Tooltip("割れるときのSE")]
    private AudioClip _break = null;
    /// <summary>当たり判定をフィルタリングする</summary>
    private ContactFilter2D _filter;
    /// <summary>ゲームオブジェクトにアタッチされているBoxCollider2D</summary>
    private BoxCollider2D _boxCol2D;
    /// <summary>ゲームオブジェクトにアタッチされているRigidbody2D</summary>
    private Rigidbody2D _rb;

    private void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
        if (!_defaultSprite || !_changeSprite)
        {
            Debug.LogWarning("設定されていない画像があります。");
        }

        _mainAudio = GetComponent<AudioSource>();
        if (!_firstCrack || !_secondCrack || !_break)
        {
            Debug.LogWarning("設定されていない効果音があります。");
        }

        _boxCol2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _filter.layerMask = LayerMask.GetMask("Player");  //判定するLayerをPlayerに制限する
        _filter.useNormalAngle = true;                    //判定する範囲を240°～ 300°に制限する
        _filter.SetNormalAngle(240f, 300f);
    }

    private void FixedUpdate()
    {
        switch (_stateCount)  //何回踏まれたかに応じて処理を切り替える
        {
            case 0:  // 初期状態
                _mainSprite.sprite = _defaultSprite.sprite;
                break;
            case 1:  // 1回目に踏まれたとき
                _boxCol2D.isTrigger = false;
                _mainSprite.sprite = _changeSprite.sprite;
                _mainSprite.DOFade(1f, 1f);
                _mainAudio.clip = _firstCrack;
                gameObject.layer = 11;
                break;
            case 2:  // 2回目に踏まれたとき
                _mainSprite.DOFade(0.5f, 1f);
                _mainAudio.clip = _secondCrack;
                DOVirtual.DelayedCall(1.0f, () =>
                {
                    _boxCol2D.isTrigger = true;
                    gameObject.layer = default;
                    _mainAudio.clip = _break;
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
