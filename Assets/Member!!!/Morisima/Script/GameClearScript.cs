using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearScript : MonoBehaviour
{
    public void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //�N���A��ʂֈڍs
            SceneManager.LoadScene("ClearScene");
        }
    }
}