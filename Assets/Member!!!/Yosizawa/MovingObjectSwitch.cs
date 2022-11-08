using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MovingObjectSwitch : MonoBehaviour
{
    [SerializeField] GameObject _movingFloar = default;
    [SerializeField] Transform[] _point = default;
    [SerializeField] float _moveSpeed = 1.0f;
    Rigidbody2D _rbChild;
    float _nowPoint = 0;

    void Start()
    {
        _rbChild = GetComponentInChildren<Rigidbody2D>() ?? _movingFloar.AddComponent<Rigidbody2D>();

        if(_point != null && _point.Length > 0)
        {
            _rbChild.transform.position = _point[0].position;
        }
    }
}
