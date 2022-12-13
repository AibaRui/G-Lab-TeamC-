using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class SledBodyController : MonoBehaviour
{
    /// <summary></summary>
    private SledController _sled = null;
    /// <summary>Rigidbody2D型の変数</summary>
    private Rigidbody2D _rb;
    /// <summary>当たり判定を制限する</summary>
    private ContactFilter2D _filter;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //接触判定をオブジェクトの前方だけに制限する
        _filter.useNormalAngle = true;
        _filter.minNormalAngle = 177f;
        _filter.maxNormalAngle = 183f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //これがアタッチされているオブジェクトに乗ったら、Playerが一緒に動くようにする
        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            collision.gameObject.transform.SetParent(transform);
        }

        //前方だけ接触判定を行う
        if (_rb.IsTouching(_filter))
        {
            //もし、オブジェクトの速度が一定以上だったら、衝突したオブジェクトを破壊する
            if (_sled.Velocity.sqrMagnitude < _sled.MaxVelocity * 3 / 4)
            {
                _rb.AddForce(-_rb.velocity * 10, ForceMode2D.Impulse);
            }
            else
            {

            }
        }
    }
}
