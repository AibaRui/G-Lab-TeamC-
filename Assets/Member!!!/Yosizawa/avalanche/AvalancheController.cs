using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AvalancheController : MonoBehaviour
{
    [Header("ê·ïˆÇÃêiçsë¨ìx")]
    [SerializeField] float _speed = 1.0f;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    void Update()
    {
        _rb.velocity = Vector2.right * _speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag is "Player1" or "Player2")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
