using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MovingObjectSwitch : GimickBase
{
    [SerializeField, Tooltip("����������GameObject")]
    private Transform _movingObject = default;
    [SerializeField, Tooltip("�ړ��̎n�_")]
    private Transform _startPoint = default;
    [SerializeField, Tooltip("�ړ��̏I�_")]
    private Transform _endPoint = default;
    [SerializeField, Tooltip("�ǂ̂��炢�̎��Ԃ������Ĉړ������邩")]
    private float _moveTime = 1.0f;
    /// <summary>���݁APause��Ԃ����肷��t���O</summary>
    private bool _isPause = false;

    private void Start()
    {
        //_movingFloar�̏����n�_��_startPoint�ɐݒ�
        _movingObject.position = _startPoint.position;
        //_movingFloar��_startPoint��_endPoint�Ƃ̊Ԃŉ���������
        _movingObject.DOMove(_endPoint.position, _moveTime)
                    .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //_movingFloar���ꎞ��~�����Ă���
        _movingObject.DOPause();
        _isPause = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_isPause)
        {
            if (collision.gameObject.tag == "Hot")
            {
                //Hot�̃I�[��������������A�ĊJ����
                _movingObject.DOPlay();
            }
            else if (collision.gameObject.tag == "Cool")
            {
                //Cool�̃I�[��������������A�ꎞ��~����
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
