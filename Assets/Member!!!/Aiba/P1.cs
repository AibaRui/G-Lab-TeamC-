using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1 : MonoBehaviour
{
    [SerializeField] float _speed = 3;
    [SerializeField] float _jumpPower = 5;
    [SerializeField] GroundCheck _groundCheck;

    [SerializeField] GameObject[] _eria = new GameObject[1];
    int count = 0;

    Rigidbody2D _rb;
    void Start()
    {
        _eria[count].SetActive(true);
        _groundCheck = _groundCheck.GetComponent<GroundCheck>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _eria[count].SetActive(false);
            count++;
            if (count == _eria.Length)
            {
                count = 0;
            }
            _eria[count].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W) && _groundCheck.IsGround)
        {
            _rb.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
}
