using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public void Onclick()
    {
        //タイトルシーンへ移行
        SceneManager.LoadScene("TitleScene");
    }
}