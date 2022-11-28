using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class SledBodyController : MonoBehaviour
{
    private SledController _sled;
    private Rigidbody2D _rb;
    private ContactFilter2D _filter;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _filter.useNormalAngle = true;
        _filter.minNormalAngle = 177f;
        _filter.maxNormalAngle = 183f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            collision.gameObject.transform.SetParent(transform);
        }
        
        if (_rb.IsTouching(_filter))
        {

        }
    }
}
