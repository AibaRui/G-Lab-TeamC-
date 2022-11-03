using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Button : MonoBehaviour
{
    public void Onclick()
    {
        //ステージ1へ移行
        SceneManager.LoadScene("PlayScene");
    }
}