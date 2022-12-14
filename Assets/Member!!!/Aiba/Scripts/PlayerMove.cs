using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    [RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{

    [Header("移動速度")]
    [Tooltip("移動速度")] [SerializeField] float _speed = 7;

    [Header("ジャンプパワー")]
    [Tooltip("ジャンプパワー")] [SerializeField] float _jumpPower = 5;

    [Header("ジャンプ可能にする床のレイヤーを選ぶ。複数選択可能")]
    [SerializeField] LayerMask _layerGround;

    [Header("設置判定の長さ")]
    [Tooltip("設置判定の長さ")] [SerializeField] public float _groundCheckLine = 1.5f;

    [Header("ジャンプの音")]
    [SerializeField] AudioSource _audJump;

    [Header("歩く音")]
    [SerializeField] AudioClip _moveAud;
 
    bool _isGround = false;

    Animator _anim;
    Rigidbody2D _rb;
    AudioSource _aud;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _aud = GetComponent<AudioSource>();
    }

    public void MoveController(int playerNumber)
    {
        float h = Input.GetAxisRaw($"PlayerMove{playerNumber}");
        Vector2 velo = new Vector2(h * _speed, _rb.velocity.y);
        _rb.velocity = velo;

        //アニメーション
        if (h != 0) _anim.SetBool("Run", true);
        else _anim.SetBool("Run", false);
    }

    public void MoveKeybord(int playernumber)
    {
        if (playernumber == 1)
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
        if (playernumber == 2)
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
            _rb.velocity = velo; 
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

    public void JumpKeybord()
    {
        if (GroundCheck())
        {
            if (gameObject.tag == "Player1")
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
                    _anim.Play("Player1_Jump");
                    _audJump.Play();
                }
            }
            else if (gameObject.tag == "Player2")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
                    _anim.Play("Player2_Jump");
                    _audJump.Play();
                }
            }

        }
    }



    public void JumpController(int playerNumber)
    {
        if (GroundCheck())
        {
            if (Input.GetButtonDown($"PlayerJump{playerNumber}"))
            {
                _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
                _anim.Play($"Player{playerNumber}_Jump");
                _audJump.Play();
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

    void MoveAudio()
    {
        _aud.PlayOneShot(_moveAud);
    }

}
