using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class SledController : MonoBehaviour
{
    [SerializeField, Tooltip("�ő呬�x")]
    private float _maxSpeed = 1f;
    [SerializeField, Tooltip("�I�[������p��Collider")]
    private Collider2D _judgeCol;
    /// <summary>Rigidbody2D</summary>
    private Rigidbody2D _rb;

    private void Start()
    {
        if (_judgeCol is null)
        {
            Debug.LogWarning("����p�̃R���C�_�[��assign����Ă��܂���");
        }
        _judgeCol.isTrigger = true;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag is "Player1" or "Player2")
        {
            collision.transform.SetParent(transform);
        }

        if(collision.gameObject.tag is "")
        {
            
        }
    }
}
