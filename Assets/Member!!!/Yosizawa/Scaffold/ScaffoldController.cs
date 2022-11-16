using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]

public class ScaffoldController : MonoBehaviour
{
    [Header("元の状態に回復するまでの時間")]
    [SerializeField, Range(1f, 7f), Tooltip("元の状態に回復するまでの時間")] float _interval = 1.0f;
    //[Header("デフォルトのイラスト")]
    //[SerializeField] SpriteRenderer _firstSprite = default;
    //[Header("1度踏まれた状態の画像")]
    //[SerializeField] SpriteRenderer _secondSprite = default;
    float _time = 0.0f;
    /// <summary>このゲームオブジェクトが踏まれた回数</summary>
    int _stateCount = 0;
    /// <summary>描画するSprite</summary>
    SpriteRenderer _mainSprite;
    /// <summary>当たり判定をフィルタリングする</summary>
    ContactFilter2D _filter;
    BoxCollider2D _boxCol2D;
    Rigidbody2D _rb;

    void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
        _boxCol2D = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _filter.layerMask = LayerMask.GetMask("Player");  //判定するLayerをPlayerに制限する
        _filter.useNormalAngle = true;                    //判定する範囲を240°～ 300°に制限する
        _filter.SetNormalAngle(240f, 300f);
    }

    void Update()
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

    //Playerが床に乗ったら、_stateCountを増加させる
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            if (_stateCount < 2 && _rb.IsTouching(_filter)) _stateCount++;
        }
        Debug.Log(_stateCount);
    }

    //「固める」オーラが当たったら、_stateCountを減少させる
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cool" && _stateCount >= 1)
        {
            _time += Time.deltaTime;
            if (_time >= _interval)
            {
                _stateCount--;
                _time = 0;
                Debug.Log(_stateCount);
            }
        }
    }
}
