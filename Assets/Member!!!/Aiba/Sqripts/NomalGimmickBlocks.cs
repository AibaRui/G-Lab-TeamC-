using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalGimmickBlocks : GimickBase
{
    [Header("オーラによって変化するまでの時間")]
    [SerializeField] float _timeLimit = 5f;

    [SerializeField] float _timeCount = 0;

    [Header("溶けてる時のイラスト")]
    [SerializeField] SpriteRenderer _spriteWater;

    [Header("固まってる時のイラスト")]
    [SerializeField] SpriteRenderer _spriteSnow;

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

    bool _isPause = false;

    SpriteRenderer _sprite;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
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
                    _timeCount += Time.deltaTime;

                    //一定時間当たっていたら状態を変化させる
                    if (_timeCount >= _timeLimit)
                    {
                        _brockState = BrockState.Ice;
                        _timeCount = 0;
                        ChangeBlock();
                    }
                }
            }
            //熱のオーラだけが当たっているとき
            if (hot && !cool)
            {
                //状態が氷だったら
                if (_brockState == BrockState.Ice)
                {
                    _timeCount += Time.deltaTime;

                    //一定時間当たっていたら状態を変化させる
                    if (_timeCount >= _timeLimit)
                    {
                        _brockState = BrockState.Hot;
                        _timeCount = 0;
                        ChangeBlock();
                    }
                }
            }

            //どっちも当たっていなかったら
            if (!hot && !cool)
            {
                //Debug.Log("no");
                //時間が以上だったら、元に戻す
                if (_timeCount > 0)
                {
                    _timeCount -= Time.deltaTime;
                    if (_timeCount < 0)
                    {
                        _timeCount = 0;
                    }
                }
            }
        }
    }

    //Stateの状態に応じて、形状を変える
    void ChangeBlock()
    {
        if (_brockState == BrockState.Hot)
        {
            _sprite.sprite = _spriteWater.sprite;
            gameObject.layer = _hotLayer;

        }
        else
        {
            _sprite.sprite = _spriteSnow.sprite;
            gameObject.layer = _coolLayer;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
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
