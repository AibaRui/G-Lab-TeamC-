using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalGimmickBlocks : GimickBase
{
    [Header("オーラによって変化するまでの時間")]
    [SerializeField] float _timeLimit = 5f;

    private float _timeCountCool = 0;
    private float _timeCountHot = 0;


    [Header("溶けてる時のイラスト")]
    [SerializeField] Sprite _spriteWater;

    [Header("固まってる時のイラスト")]
    [SerializeField] Sprite _spriteSnow;

    [Header("氷の状態の時のレイヤー(Playerレイヤーと当たる)")]
    [Tooltip("氷の状態の時のレイヤー(Playerレイヤーと当たる)")] [SerializeField] int _coolLayer;

    [Header("水の状態の時のレイヤー(Playerレイヤーと当たらない)")]
    [Tooltip("水の状態の時のレイヤー(Playerレイヤーと当たらない)")] [SerializeField] int _hotLayer;

    [Header("熱の能力のタグ")]
    [SerializeField] string _hotTagName;

    [Header("冷気の能力のタグ")]
    [SerializeField] string _coolTagName;


    //現在、熱のオーラにあたっているかどうかを
    bool hot;
    //現在、冷気のオーラにあたっているかどうかを
    bool cool;

    [Header("初期の状態を決める")]
    [SerializeField] BrockState _brockState = BrockState.Ice;

    [Header("溶けた時の音")]
    [SerializeField] AudioClip _hotSound;
    [Header("固まった時の音")]
    [SerializeField] AudioClip _coolSound;
    bool _isPause = false;

    AudioSource _aud;
    SpriteRenderer _sprite;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _aud = GetComponent<AudioSource>();
        ChangeBlock();
    }

    void Update()
    {
        if (!_isPause)
        {
            //冷気のオーラだけが当たっているとき
            if (!hot && cool)
            {
                //状態が熱だったら
                if (_brockState == BrockState.Hot)
                {
                    _timeCountCool += Time.deltaTime;

                    //一定時間当たっていたら状態を変化させる
                    if (_timeCountCool >= _timeLimit)
                    {
                        _brockState = BrockState.Ice;
                        _timeCountCool = 0;
                        ChangeBlock();
                        _aud.PlayOneShot(_coolSound);
                    }
                }
            }
            //熱のオーラだけが当たっているとき
            if (hot && !cool)
            {
                //状態が氷だったら
                if (_brockState == BrockState.Ice)
                {
                    _timeCountHot += Time.deltaTime;

                    //一定時間当たっていたら状態を変化させる
                    if (_timeCountHot >= _timeLimit)
                    {
                        _brockState = BrockState.Hot;
                        _timeCountHot = 0;
                        ChangeBlock();
                        _aud.PlayOneShot(_hotSound);
                    }
                }
            }

            //どっちも当たっていなかったら
            if (!hot && !cool)
            {
                //Debug.Log("no");
                //時間が以上だったら、元に戻す
                if (_timeCountCool > 0)
                {
                    _timeCountCool -= Time.deltaTime;
                    if (_timeCountCool < 0)
                    {
                        _timeCountCool = 0;
                    }
                }
                if (_timeCountHot > 0)
                {
                    _timeCountHot -= Time.deltaTime;
                    if (_timeCountHot < 0)
                    {
                        _timeCountHot = 0;
                    }
                }
            }
        }

       // Debug.Log(_timeCountCool);
    }

    //Stateの状態に応じて、形状を変える
    void ChangeBlock()
    {
        if (_brockState == BrockState.Hot)
        {
            _sprite.sprite = _spriteWater;
            gameObject.layer = _hotLayer;

        }
        else
        {
            _sprite.sprite = _spriteSnow;
            gameObject.layer = _coolLayer;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isPause)
        {
            if (collision.gameObject.tag == _coolTagName)
            {
                cool = true;
            }

            if (collision.gameObject.tag == _hotTagName)
            {
                hot = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _coolTagName)
        {
            cool = false;
        }

        if (collision.gameObject.tag == _hotTagName)
        {
            hot = false;
        }
    }


    enum BrockState
    {
        Ice,
        Hot,
    }



    /// <summary>ゲームオーバー時に呼ぶ。アニメーションの停止、Rigidbodyの停止、判定消し</summary>
    public override void GameOverPause()
    {
        _isPause = true;
    }

    /// <summary>一時停止時に呼ぶ。アニメーションの停止、Rigidbodyの停止、判定消しの処理を書く</summary>
    public override void Pause()
    {
        _isPause = true;
    }
    /// <summary>ゲーム再開時に呼ぶ。アニメーションの再開、Rigidbodyの再開、判定再開を書く</summary>
    public override void Resume()
    {
        _isPause = false;
    }


}
