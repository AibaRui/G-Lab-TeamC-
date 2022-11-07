using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

class MovingObjectSwitch : MonoBehaviour
{
    [Header("動かしたいGameObjectを入れる")][Tooltip("動かしたい対象")]
    [SerializeField] Transform _movingFloar = default;
    [Header("GameObjectをどの地点まで移動させるか")][Tooltip("移動の終点")]
    [SerializeField] Transform[] _point = default;
    int count = 1;
    bool isThermo = false;
    [Header("どのくらいの時間をかけて移動させるか")]
    [SerializeField] float _moveTime = 1.0f;
    void Update()
    {
        if(isThermo)
        {
            _movingFloar.DORestart();
            _movingFloar.DOMove(_point[count].position, _moveTime);
            if(Vector2.Distance(_movingFloar.position, _point[count].position) <= 1.0f)
            {
                _movingFloar.DOKill();
                count++;
                Debug.Log($"{count}");
                if (count >= _point.Length) count = 0;
            }
        }
        else
        {
            _movingFloar.DOPause();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (_movingFloar != null)
        {
            if (collision.gameObject.tag == "Hot")
            {
                isThermo = true;
            }
            else if (collision.gameObject.tag == "Cool")
            {
                isThermo = false;
            }
        }
        else Debug.LogError("動かしたいGameObjectが見つかりません");
    }
}
