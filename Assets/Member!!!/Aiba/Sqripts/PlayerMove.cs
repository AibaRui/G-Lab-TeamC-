using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("プレイヤーの番号")]
    [Tooltip("プレイヤーの番号")] [SerializeField] int _playerNumber = 1;

    [Header("移動速度")]
    [Tooltip("移動速度")] [SerializeField] float _speed = 7;


    [Header("ジャンプパワー")]
    [Tooltip("ジャンプパワー")] [SerializeField] float _jumpPower = 5;


    [SerializeField] LayerMask _layerGround;

    [Header("設置判定の長さ")]
    [Tooltip("設置判定の長さ")] [SerializeField] float _groundCheckLine = 1.5f;
    bool _isGround = false;

    [SerializeField] Sousa _sousa = Sousa.Controller;


    Animator _anim;
    Rigidbody2D _rb;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (_sousa == Sousa.Controller)
        {
            MoveController();
            JumpController();
        }
        else
        {
            MoveKeybord();
            JumpKeybord();
        }


        Vector2 start = transform.position;
        Vector2 end = transform.position + (-transform.up) * _groundCheckLine;
        Debug.DrawLine(start, end);
    }

    private void LateUpdate()
    {
        _anim.SetFloat("SpeedY", _rb.velocity.y);

    }


    private void MoveController()
    {
        float h = Input.GetAxisRaw($"PlayerMove{_playerNumber}");
        Vector2 velo = new Vector2(h * _speed, _rb.velocity.y);
        _rb.velocity = velo;

        //アニメーション
        if (h != 0) _anim.SetBool("Run", true);
        else _anim.SetBool("Run", false);
    }

    void MoveKeybord()
    {
        if (_playerNumber == 1)
        {
            float h = 0;
            if (Input.GetKey(KeyCode.D))
            {
                h = 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                h = -1;
            }

            Vector2 velo = new Vector2(h * _speed, _rb.velocity.y);
            _rb.velocity = velo;
            //アニメーション
            if (h != 0)
            {
                _anim.SetBool("Run", true);
                transform.localScale = new Vector3(h, 1, 1);
            }
            else _anim.SetBool("Run", false);
        }
        if (_playerNumber == 2)
        {
            float h = 0;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                h = 1;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                h = -1;
            }

            Vector2 velo = new Vector2(h * _speed, _rb.velocity.y);
            //アニメーション
            if (h != 0)
            {
                _anim.SetBool("Run", true);
                transform.localScale = new Vector3(h, 1, 1);
            }
            else _anim.SetBool("Run", false);
        }

        //  float h = Input.GetAxisRaw("Horizontal");


    }

    void JumpKeybord()
    {
        if (GroundCheck())
        {
            if (gameObject.tag == "Player1")
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
                    _anim.Play("Player1_Jump");
                }
            }
            else if (gameObject.tag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
                    _anim.Play("Player1_Jump");
                }
            }

        }
    }



    private void JumpController()
    {
        if (GroundCheck())
        {
            if (Input.GetButtonDown($"PlayerJump{_playerNumber}"))
            {
                _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
                _anim.Play($"Player{_playerNumber}_Jump");
            }
        }

    }


    bool GroundCheck()
    {
        Vector2 start = transform.position;
        Vector2 end = transform.position + (-transform.up) * _groundCheckLine;
        _isGround = Physics2D.Linecast(start, end, _layerGround);

        _anim.SetBool("IsGround", _isGround);
        return _isGround;
    }

    enum Sousa
    {
        Controller,
        KeyBord,
    }
}
