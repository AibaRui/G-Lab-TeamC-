using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Button : MonoBehaviour
{
    public void Onclick()
    {
        //ステージ2へ移行
        SceneManager.LoadScene("PlayScene");
    }
}