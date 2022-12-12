using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("生成したいゲームオブジェクトを設定する")]
    private GameObject _go;
    [SerializeField, Tooltip("最大同時存在数")]
    private int _maxExist = 0;
    [SerializeField, Tooltip("生成間隔")]
    private float _interval = 1.0f;
    /// <summary>時間を計測するタイマー</summary>
    float _timer = 0f;

    private void Update()
    {
        //現在のオブジェクトの数が指定した数より多かったら、生成をしない
        if (ExistCount())
        {
            _timer += Time.deltaTime;

            if (_timer >= _interval)
            {
                //生成したオブジェクトを数えるために、Generatorの子オブジェクトにしておく
                GameObject go = Instantiate(_go);
                go.transform.SetParent(transform);
                go.transform.position = transform.position;
                _timer = 0;
            }
        }
    }

    /// <summary>現在、いくつのオブジェクトが存在しているかを調べる</summary>
    /// <returns>オブジェクトの数が一定以上だったら[false]、そうでないなら[true]</returns>
    private bool ExistCount()
    {
        int count = 0;

        foreach (Transform child in transform)
        {
            count++;
        }
        
        if(count < _maxExist) { return true; }
        else { return false; }
    }
}
