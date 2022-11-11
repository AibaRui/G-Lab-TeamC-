using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IcicleController : MonoBehaviour
{
    [Header("óéâ∫ë¨ìx")]
    [SerializeField] float _fallSpeed = 1.0f;
    [Header("GameObjectè¡é∏éûÇ…çƒê∂Ç∑ÇÈAnimetion")]
    [SerializeField] GameObject _onDestroyAnimation = default;
    [SerializeField] bool _isGizmo = false;
    [SerializeField] float _angle = 1.0f;
    [SerializeField] Vector2 _checkArea;
    RaycastHit2D _hit;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            _rb.gravityScale = _fallSpeed;
        }
    }

    void OnDrawGizmos()
    {
        if (_isGizmo == false) return;

        bool isHit = Physics2D.BoxCast(transform.position, _checkArea, _angle, Vector2.down);
    }
}
