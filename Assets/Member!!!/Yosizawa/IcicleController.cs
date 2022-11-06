using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IcicleController : MonoBehaviour
{
    [Header("落下速度")]
    [SerializeField] float _fallSpeed = 1.0f;
    [Header("GameObject消失時に再生するAnimetion")]
    [SerializeField] GameObject _onDestroyAnimation = default;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _rb.gravityScale = _fallSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_onDestroyAnimation == null) Debug.LogError("GameObject消失時のアニメーションがありません");
        else Instantiate(_onDestroyAnimation);
        Destroy(gameObject);
    }
}
