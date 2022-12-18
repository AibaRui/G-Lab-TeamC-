using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMunu : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    bool _isPause = false;

    void Start()
    {
        
    }
    void Update()
    {
        // ESC キーが押されたら一時停止・再開を切り替える
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("PauseButtun"))
        {
            _isPause = !_isPause;
            _panel.SetActive(_isPause);
        }
    }
}
