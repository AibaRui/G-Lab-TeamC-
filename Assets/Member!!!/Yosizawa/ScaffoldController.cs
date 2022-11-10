using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]

public class ScaffoldController : MonoBehaviour
{
    /// <summary>描画するSprite</summary>
    SpriteRenderer _mainSprite;
    //[SerializeField] BoxCollider2D _col;
    //[SerializeField] BoxCollider2D _judgeCol;
    [SerializeField] float _interval = 1.0f;
    float _time = 1.0f;
    //bool _countFrag;
    /// <summary>このゲームオブジェクトが踏まれた回数</summary>
    int _stateCount = 0;

    void Start()
    {
        _mainSprite = GetComponent<SpriteRenderer>();
        //_col = GetComponent<BoxCollider2D>();
        //_judgeCol = GetComponentInChildren<BoxCollider2D>();
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
                //int count = 0;
                //Mathf.Clamp01(count);
                //if(count == 0)
                //{
                //    count++;
                    DOVirtual.DelayedCall(1.0f, () =>
                    {
                        //_col.isTrigger = true;
                        //_judgeCol.enabled = false;
                        gameObject.SetActive(false);
                    }, false);
                //}
                //else if(count == 1)
                //{
                //    if (_countFrag == true)
                //    {
                //        count--;
                //        _countFrag = false;
                //    }
                //}
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            if(_stateCount < 2) _stateCount++;
        }
        else if(collision.gameObject.tag == "Cool")
        {
            //_time += Time.deltaTime;
            if (_stateCount > 0/* && _time >= _interval*/)
            {
                //_col.isTrigger = false;
                //_judgeCol.enabled = true;
                ////_countFrag = true;
                _stateCount--;
                _time = 0;
            }
        }
        Debug.Log(_stateCount);
    }
}
