using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1 : MonoBehaviour
{
    [Header("移動速度")]
    [Tooltip("移動速度")] [SerializeField] float _speed = 3;

    [Header("ジャンプパワー")]
    [Tooltip("ジャンプパワー")] [SerializeField] float _jumpPower = 5;

    [Header("能力の形")]
    [Tooltip("能力の形")] [SerializeField] GameObject[] _eria = new GameObject[1];
    int count = 0;

    [SerializeField] LayerMask _layerGround;

    [Header("設置判定の長さ")]
    [Tooltip("設置判定の長さ")] [SerializeField] float _groundCheckLine = 1.5f;
    bool _isGround = false;




    Rigidbody2D _rb;
    void Start()
    {
        _eria[count].SetActive(true);
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _eria[count].SetActive(false);
            count++;
            if (count == _eria.Length)
            {
                count = 0;
            }
            _eria[count].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W) && GroundCheck())
        {
            _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
        }



        Vector2 start = transform.position;
        Vector2 end = transform.position + (-transform.up) * _groundCheckLine;
        Debug.DrawLine(start, end);
    }


    bool GroundCheck()
    {
        Vector2 start = transform.position;
        Vector2 end = transform.position + (-transform.up) * _groundCheckLine;
        _isGround = Physics2D.Linecast(start, end,_layerGround);

        return _isGround;
    }


}
