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
            // Enter �ŃN���A��ʂֈڍs
            SceneManager.LoadScene("ClearScene");
        }
    }
}
