using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("�I�[���̐؂�ւ��̑�������ł��邩")]
    [SerializeField] Sousa _sousa = Sousa.Controller;

    [Header("�v���C���[�̔ԍ��B�Ȃ�1�A�ԂȂ�2")]
    [Tooltip("�v���C���[�̔ԍ�")] [SerializeField] int _playerNumber = 1;

    [Header("�S�[���̃^�O�̖��O")]
    [SerializeField] string _goalTagName = "";

    [Header("����̃^�O�̖��O")]
    [SerializeField] string _nadareTagName = "";

    [Header("���̓��Hieralchy����v���C���[���g������΂����B")]
    [SerializeField]
    PlayerChangeAura _playerChangeAura;

    [SerializeField]
    PlayerMove _playerMove;

    [SerializeField]
    DamagePlayer _playerDamage;

    GameManager _gm;

    private bool _isPause;

    Vector2 _saveVelo;

    Rigidbody2D _rb;
    Animator _anim;

    PauseManager _pauseManager = default;

    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
        _gm = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isPause)
        {
            if (_sousa == Sousa.Controller)
            {
                if (!_playerDamage.IsKnockBack)
                {
                    _playerMove.MoveController(_playerNumber);
                    _playerMove.JumpController(_playerNumber);
                }


                _playerChangeAura.CheckChenge();
                _playerChangeAura.ChangeAuraControllerJoistick(_playerNumber);
            }
            else if (_sousa == Sousa.KeyBord)
            {
                if (!_playerDamage.IsKnockBack)
                {
                    _playerMove.MoveKeybord(_playerNumber);
                    _playerMove.JumpKeybord();
                }

                _playerChangeAura.ChangeAuraKeyBord(_playerNumber);
                _playerChangeAura.CheckChenge();
            }
            _playerDamage.CountTime();
        }

        Vector2 start = transform.position;
        Vector2 end = transform.position + (-transform.up) * _playerMove._groundCheckLine;
        Debug.DrawLine(start, end);
    }


    private void FixedUpdate()
    {

    }
    private void LateUpdate()
    {
        _anim.SetFloat("SpeedY", _rb.velocity.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _goalTagName)
        {
            _gm.GameClear();
        }

        if (collision.gameObject.tag == _nadareTagName)
        {
            _gm.GameOver();
        }

    }

    enum Sousa
    {
        Controller,
        KeyBord,
    }


    ///////Parse����/////

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.GameEnd += GameEndPauseResume;
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= GameEndPauseResume;
    }


    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    void GameEndPauseResume(bool isPause)
    {
        if (isPause)
        {
            GameOverPause();
        }
    }


    /// <summary>�Q�[���I�[�o�[���ɌĂԁB�A�j���[�V�����̒�~�ARigidbody�̒�~�A�������</summary>
    void GameOverPause()
    {
        _isPause = true;
        _anim.enabled = false;
        _saveVelo = _rb.velocity;
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
    }

    /// <summary>�ꎞ��~���ɌĂԁB�A�j���[�V�����̒�~�ARigidbody�̒�~�A��������̏���������</summary>
    void Pause()
    {
        _isPause = true;
        _anim.enabled = false;
        _saveVelo = _rb.velocity;
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
    }
    /// <summary>�Q�[���ĊJ���ɌĂԁB�A�j���[�V�����̍ĊJ�ARigidbody�̍ĊJ�A����ĊJ������</summary>
    void Resume()
    {
        _isPause = false;
        _anim.enabled = true;
        _rb.isKinematic = false;
        _rb.velocity = _saveVelo;


    }


}
