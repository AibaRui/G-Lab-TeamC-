using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowtoplayButton : MonoBehaviour
{
    public void Onclick()
    {
        //遊び方シーンへ移行
        SceneManager.LoadScene("HowtoplayScene");
    }
}