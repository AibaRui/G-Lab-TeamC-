using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]

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
    /// <summary>�Q�[���I�u�W�F�N�g��SpriteRenderer</summary>
    private SpriteRenderer _mainSprite = null;
    [SerializeField, Tooltip("�X�C�b�`��OFF�̉摜")]
    private Sprite _offSprite = null;
    [SerializeField, Tooltip("�X�C�b�`��ON�̉摜")]
    private Sprite _onSprite = null;
    /// <summary>�X�C�b�`��؂�ւ�������SE</summary>
    private AudioSource _audio = null;
    private int _hotCount = 0;
    private int _coolCount = 0;
    /// <summary>���݁APause��Ԃ����肷��t���O</summary>
    private bool _isPause = false;

    private void Start()
    {
        //
        _mainSprite = GetComponent<SpriteRenderer>();
        _mainSprite.sprite = _offSprite;

        //_movingFloar�̏����n�_��_startPoint�ɐݒ�
        _movingObject.position = _startPoint.position;

        //_movingFloar��_startPoint��_endPoint�Ƃ̊Ԃŉ���������
        _movingObject.DOMove(_endPoint.position, _moveTime)
                    .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        //_movingFloar���ꎞ��~�����Ă���
        _movingObject.DOPause();

        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_isPause)
        {
            //Hot�̃I�[��������������A�ĊJ����
            if (collision.gameObject.tag == "Hot" && _hotCount == 0)
            {
                _mainSprite.sprite = _onSprite;
                _audio.Play();
                _movingObject.DOPlay();
                _hotCount++;
                _coolCount = 0;
            }

            //Cool�̃I�[��������������A�ꎞ��~����
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
