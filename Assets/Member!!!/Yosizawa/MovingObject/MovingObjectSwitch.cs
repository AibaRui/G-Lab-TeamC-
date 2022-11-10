using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MovingObjectSwitch : MonoBehaviour
{
    [Header("動かしたいゲームオブジェクトを入れるところ")]
    [SerializeField, Tooltip("動かしたいGameObject")] Transform _movingObject = default;
    [Header("ゲームオブジェクトの始点")]
    [SerializeField] Transform _startPoint = default;
    [Header("ゲームオブジェクトの終点")]
    [SerializeField] Transform _endPoint = default;
    [Header("どのくらいの時間をかけて移動させたいか")]
    [SerializeField] float _moveTime = 1.0f;

    void Start()
    {
        //_movingFloarの初期地点を_startPointに設定
        _movingObject.position = _startPoint.position;
        //_movingFloarを_startPointと_endPointとの間で往復させる
        _movingObject.DOMove(_endPoint.position, _moveTime)
                    .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //_movingFloarを一時停止させておく
        _movingObject.DOPause();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hot")
        {
            //Hotのオーラが当たったら、再開する
            _movingObject.DOPlay();
        }
        else if (collision.gameObject.tag == "Cool")
        {
            //Coolのオーラが当たったら、一時停止する
            _movingObject.DOPause();
        }
    }
}
