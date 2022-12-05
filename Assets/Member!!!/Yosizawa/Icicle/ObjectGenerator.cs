using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("生成したいゲームオブジェクトを設定する")]
    private GameObject _go;
    [SerializeField, Tooltip("最大同時存在数")]
    private int _maxOfExist = 0;
    /// <summary>生成間隔</summary>
    private float _interval = 1.0f;
    /// <summary>時間を計測するタイマー</summary>
    float _timer = 0f;

    private void Update()
    {
        //現在のオブジェクトの数が指定した数より多かったら、生成をしない
        if (ExistCount() > _maxOfExist) return;

        _timer += Time.deltaTime;

        if (_timer >= _interval)
        {
            //生成したオブジェクトを数えるために、Generatorの子オブジェクトにしておく
            GameObject go = Instantiate(_go);
            go.transform.SetParent(transform);
            go.transform.position = transform.position;
        }
    }

    /// <summary>現在、いくつのオブジェクトが存在しているかを調べる</summary>
    /// <returns>現在、存在しているオブジェクトの数</returns>
    private int ExistCount()
    {
        int count = 0;

        foreach (Transform child in transform)
        {
            count++;
        }
        return count;
    }
}
