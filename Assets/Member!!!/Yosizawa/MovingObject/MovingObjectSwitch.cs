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
    private Rigidbody2D _objectRb = null;

    private void Start()
    {
        //assign���ꂽ�Q�[���I�u�W�F�N�g��Rigidbody2D���A�^�b�`����Ă��邱�Ƃ��m�񂷂�
        if (_movingObject.TryGetComponent(out Rigidbody2D rb)) _objectRb = rb;
        else _movingObject.gameObject.AddComponent<Rigidbody2D>();
        _objectRb.gravityScale = 0;
        _objectRb.constraints = RigidbodyConstraints2D.FreezeAll;

        //_movingFloar�̏����n�_��_startPoint�ɐݒ�
        _movingObject.position = _startPoint.position;

        //_movingFloar��_startPoint��_endPoint�Ƃ̊Ԃŉ���������
        _movingObject.DOMove(_endPoint.position, _moveTime)
                    .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        //_movingFloar���ꎞ��~�����Ă���
        _movingObject.DOPause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_isPause)
        {
            //Hot�̃I�[��������������A�ĊJ����
            if (collision.gameObject.tag == "Hot")
            {
                _movingObject.DOPlay();
            }

            //Cool�̃I�[��������������A�ꎞ��~����
            else if (collision.gameObject.tag == "Cool")
            {
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
        _isPause = false;
        _movingObject.DOPlay();
    }
}
