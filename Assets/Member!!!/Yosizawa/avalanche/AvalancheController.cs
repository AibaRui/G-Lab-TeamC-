using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AvalancheController : GimickBase
{
    [Header("ê·ïˆÇÃêiçsë¨ìx")]
    [SerializeField] float _speed = 1.0f;
    Rigidbody2D _rb;
    Vector2 _saveVelocity;

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

    public override void GameOverPause()
    {
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Pause()
    {
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Resume()
    {
        _rb.simulated = true;
        _rb.WakeUp();
        _rb.velocity = _saveVelocity;
    }
}
