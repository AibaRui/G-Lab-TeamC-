using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MovingObjectSwitch : MonoBehaviour
{
    [SerializeField] Transform _movingFloar = default;
    [SerializeField] Transform[] _point = default;
    [SerializeField] float _moveSpeed = 1.0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hot")
        {
            _movingFloar.DORestart();
            _movingFloar.DOMove(_point[1].position, _moveSpeed)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else if (collision.gameObject.tag == "Cool")
        {
            _movingFloar.DOPause();
        }
    }
}
