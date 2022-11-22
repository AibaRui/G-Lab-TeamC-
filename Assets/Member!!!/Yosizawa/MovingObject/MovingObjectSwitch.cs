using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MovingObjectSwitch : GimickBase
{
    [SerializeField, Tooltip("動かしたいGameObject")]
    private Transform _movingObject = default;
    [SerializeField, Tooltip("移動の始点")]
    private Transform _startPoint = default;
    [SerializeField, Tooltip("移動の終点")]
    private Transform _endPoint = default;
    [SerializeField, Tooltip("どのくらいの時間をかけて移動させるか")]
    private float _moveTime = 1.0f;
    /// <summary>現在、Pause状態か判定するフラグ</summary>
    private bool _isPause = false;

    private void Start()
    {
        //_movingFloarの初期地点を_startPointに設定
        _movingObject.position = _startPoint.position;
        //_movingFloarを_startPointと_endPointとの間で往復させる
        _movingObject.DOMove(_endPoint.position, _moveTime)
                    .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //_movingFloarを一時停止させておく
        _movingObject.DOPause();
        _isPause = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_isPause)
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

    public override void GameOverPause()
    {
        _movingObject.DOPause();
        _isPause = true;
    }

    public override void Pause()
    {
        _movingObject.DOPause();
        _isPause = true;
    }

    public override void Resume()
    {
        _movingObject.DOPlay();
        _isPause = false;
    }
}
