using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class SledController : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    public float _speed = 1f;
    [SerializeField, Tooltip("実際に動かすオブジェクト")]
    private GameObject _movingObj;
    /// <summary>Rigidbody2D</summary>
    private Rigidbody2D _objRb;

    private void Start()
    {
        _movingObj.transform.SetParent(transform);
        if (!_movingObj.GetComponent<SledBodyController>())
        {
            _movingObj.AddComponent<SledBodyController>();
        }
        _objRb = _movingObj.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag is "Hot")
        {
            _objRb.AddForce(new Vector2(_speed, 0), ForceMode2D.Force);
        }
        if (collision.gameObject.tag is "Cool")
        {
            _objRb.AddForce(new Vector2(-_speed, 0), ForceMode2D.Force);
        }
    }
}
