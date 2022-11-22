using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class AvalancheController : GimickBase
{
    [SerializeField, Tooltip("ê·ïˆÇÃêiçsë¨ìx")]
    private float _speed = 1.0f;
    private Rigidbody2D _rb;
    private Vector2 _saveVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        _rb.velocity = Vector2.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
