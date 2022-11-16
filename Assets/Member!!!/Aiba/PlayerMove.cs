using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("�v���C���[�̔ԍ�")]
    [Tooltip("�v���C���[�̔ԍ�")] [SerializeField] int _playerNumber = 1;

    [Header("�ړ����x")]
    [Tooltip("�ړ����x")] [SerializeField] float _speed = 7;


    [Header("�W�����v�p���[")]
    [Tooltip("�W�����v�p���[")] [SerializeField] float _jumpPower = 5;


    [SerializeField] LayerMask _layerGround;

    [Header("�ݒu����̒���")]
    [Tooltip("�ݒu����̒���")] [SerializeField] float _groundCheckLine = 1.5f;
    bool _isGround = false;

    [SerializeField] Sousa _sousa = Sousa.Controller;


    Rigidbody2D _rb;
    void Start()
    {
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

    private void MoveController()
    {
        float h = Input.GetAxisRaw($"PlayerMove{_playerNumber}");
        Vector2 velo = new Vector2(h * _speed, _rb.velocity.y);
        _rb.velocity = velo;
    }

    void MoveKeybord()
    {
        float h = Input.GetAxisRaw("Horizontal");
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
