using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Enter でクリア画面へ移行
            SceneManager.LoadScene("ClearScene");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Enter でクリア画面へ移行
            SceneManager.LoadScene("GameOverScene");
        }

    }
}
