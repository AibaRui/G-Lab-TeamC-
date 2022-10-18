using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2 : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    [SerializeField] float _jumpPower = 5;

    [SerializeField] GameObject[] _eria = new GameObject[1];
    int count = 0;
    [SerializeField] GroundCheck _groundCheck;
    Rigidbody2D _rb;
    void Start()
    {
        _groundCheck = _groundCheck.GetComponent<GroundCheck>();
        _eria[count].SetActive(true);
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            _eria[count].SetActive(false);
            count++;
            if (count == _eria.Length)
            {
                count = 0;
            }
            _eria[count].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && _groundCheck.IsGround)
        {
            _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
