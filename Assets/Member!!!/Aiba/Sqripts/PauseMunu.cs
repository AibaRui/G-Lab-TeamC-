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
        // ESC �L�[�������ꂽ��ꎞ��~�E�ĊJ��؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;
            _panel.SetActive(_isPause);
        }
    }
}
