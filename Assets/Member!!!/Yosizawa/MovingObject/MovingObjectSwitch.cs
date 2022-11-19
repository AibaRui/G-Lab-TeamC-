using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MovingObjectSwitch : GimickBase
{
    [Header("�����������Q�[���I�u�W�F�N�g������Ƃ���")]
    [SerializeField, Tooltip("����������GameObject")] Transform _movingObject = default;
    [Header("�Q�[���I�u�W�F�N�g�̎n�_")]
    [SerializeField] Transform _startPoint = default;
    [Header("�Q�[���I�u�W�F�N�g�̏I�_")]
    [SerializeField] Transform _endPoint = default;
    [Header("�ǂ̂��炢�̎��Ԃ������Ĉړ�����������")]
    [SerializeField] float _moveTime = 1.0f;
    bool _isPause = false;

    void Start()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isPause)
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
        _isPause = false;
    }

    public override void Pause()
    {
        _movingObject.DOPause();
        _isPause = false;
    }

    public override void Resume()
    {
        _movingObject.DOPlay();
        _isPause = true;
    }
}
