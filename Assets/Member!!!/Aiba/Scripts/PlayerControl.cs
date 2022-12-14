using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("オーラの切り替えの操作を何でするか")]
    [SerializeField] Sousa _sousa = Sousa.Controller;

    [Header("プレイヤーの番号。青なら1、赤なら2")]
    [Tooltip("プレイヤーの番号")] [SerializeField] int _playerNumber = 1;

    [Header("ゴールのタグの名前")]
    [SerializeField] string _goalTagName = "";

    [Header("雪崩のタグの名前")]
    [SerializeField] string _nadareTagName = "";

    [Header("この二つはHieralchyからプレイヤー自身をつければいい。")]
    [SerializeField]
    PlayerChangeAura _playerChangeAura;

    [SerializeField]
    PlayerMove _playerMove;

    [SerializeField]
    PlayerDamage _playerDamage;

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


    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.GameEnd += GameEndPauseResume;
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
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


    /// <summary>ゲームオーバー時に呼ぶ。アニメーションの停止、Rigidbodyの停止、判定消し</summary>
    void GameOverPause()
    {
        _isPause = true;
        _anim.enabled = false;
        _saveVelo = _rb.velocity;
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
    }

    /// <summary>一時停止時に呼ぶ。アニメーションの停止、Rigidbodyの停止、判定消しの処理を書く</summary>
    void Pause()
    {
        _isPause = true;
        _anim.enabled = false;
        _saveVelo = _rb.velocity;
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
    }
    /// <summary>ゲーム再開時に呼ぶ。アニメーションの再開、Rigidbodyの再開、判定再開を書く</summary>
    void Resume()
    {
        _isPause = false;
        _anim.enabled = true;
        _rb.isKinematic = false;
        _rb.velocity = _saveVelo;


    }


}
