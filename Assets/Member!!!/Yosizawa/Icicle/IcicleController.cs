using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IcicleController : MonoBehaviour
{
    [Header("�������x")]
    [SerializeField] float _fallSpeed = 1.0f;
    [Header("GameObject�������ɍĐ�����Animetion")]
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
        if (_onDestroyAnimation == null) Debug.LogError("GameObject�������̃A�j���[�V����������܂���");
        else Instantiate(_onDestroyAnimation);
        Destroy(gameObject);
    }
}
