using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]

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
    /// <summary>ゲームオブジェクトのSpriteRenderer</summary>
    private SpriteRenderer _mainSprite = null;
    [SerializeField, Tooltip("スイッチがOFFの画像")]
    private Sprite _offSprite = null;
    [SerializeField, Tooltip("スイッチがONの画像")]
    private Sprite _onSprite = null;
    /// <summary>スイッチを切り替えた時のSE</summary>
    private AudioSource _audio = null;
    private int _hotCount = 0;
    private int _coolCount = 0;
    /// <summary>現在、Pause状態か判定するフラグ</summary>
    private bool _isPause = false;

    private void Start()
    {
        //
        _mainSprite = GetComponent<SpriteRenderer>();
        _mainSprite.sprite = _offSprite;

        //_movingFloarの初期地点を_startPointに設定
        _movingObject.position = _startPoint.position;

        //_movingFloarを_startPointと_endPointとの間で往復させる
        _movingObject.DOMove(_endPoint.position, _moveTime)
                    .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        //_movingFloarを一時停止させておく
        _movingObject.DOPause();

        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_isPause)
        {
            //Hotのオーラが当たったら、再開する
            if (collision.gameObject.tag == "Hot" && _hotCount == 0)
            {
                _mainSprite.sprite = _onSprite;
                _audio.Play();
                _movingObject.DOPlay();
                _hotCount++;
                _coolCount = 0;
            }

            //Coolのオーラが当たったら、一時停止する
            else if (collision.gameObject.tag == "Cool" && _coolCount == 0)
            {
                _mainSprite.sprite = _offSprite;
                _audio.Play();
                _movingObject.DOPause();
                _coolCount++;
                _hotCount = 0;
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
        _isPause = false;
        _movingObject.DOPlay();
    }
}
