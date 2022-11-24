using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("プレイヤーの番号")]
    [Tooltip("プレイヤーの番号")] [SerializeField] int _playerNumber;

    [Header("移動速度")]
    [Tooltip("移動速度")] [SerializeField] float _speed = 7;


    [Header("ジャンプパワー")]
    [Tooltip("ジャンプパワー")] [SerializeField] float _jumpPower = 5;


    [SerializeField] LayerMask _layerGround;

    [Header("設置判定の長さ")]
    [Tooltip("設置判定の長さ")] [SerializeField] float _groundCheckLine = 1.5f;
    bool _isGround = false;

    [SerializeField] Sousa _sousa = Sousa.Controller;


    [SerializeField] Rigidbody2D _rb;
    void Start()
    {
        _rb = _rb.GetComponent<Rigidbody2D>();
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

    private void MoveController()
    {
        Debug.Log(_playerNumber);
            float h = Input.GetAxisRaw($"PlayerMove{_playerNumber}");
            Vector2 velo = new Vector2(h * _speed, _rb.velocity.y);
            _rb.velocity = velo;
    }

    void MoveKeybord()
    {
        float h = 0;
        if(_playerNumber==1)
        {
            if(Input.GetKey(KeyCode.D))
            {
                h = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                h = -1;
            }

        }

        if (_playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                h = 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                h = -1;
            }

        }

        //float h = Input.GetAxisRaw("Horizontal");
        Vector2 velo = new Vector2(h * _speed, _rb.velocity.y);
        _rb.velocity = velo;
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
                }
            }
            else if (gameObject.tag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
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
            }
        }

    }


    bool GroundCheck()
    {
        Vector2 start = transform.position;
        Vector2 end = transform.position + (-transform.up) * _groundCheckLine;
        _isGround = Physics2D.Linecast(start, end, _layerGround);

        return _isGround;
    }

    enum Sousa
    {
        Controller,
        KeyBord,
    }
}
