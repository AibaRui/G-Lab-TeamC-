using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]

public class ScaffoldController : MonoBehaviour
{
    /// <summary>描画するSprite</summary>
    SpriteRenderer _mainSprite;
    [SerializeField, Tooltip("回復するまでの時間")] float _interval = 1.0f;
    [SerializeField] float _time = 0.0f;
    /// <summary>このゲームオブジェクトが踏まれた回数</summary>
    int _stateCount = 0;

    void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (_stateCount)
        {
            case 0:
                _mainSprite.color = Color.red;
                break;
            case 1:
                _mainSprite.color = Color.blue;
                break;
            case 2:
                DOVirtual.DelayedCall(1.0f, () => gameObject.SetActive(false), false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            if(_stateCount < 2) _stateCount++;
        }
        Debug.Log(_stateCount);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cool")
        {
            if (_stateCount > 0)
            {
                _time += Time.deltaTime;
                if(_time >= _interval)
                {
                    _stateCount--;
                    _time = 0;
                }
            }
        }
    }
}
