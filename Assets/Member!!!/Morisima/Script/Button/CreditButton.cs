using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{
    public void Onclick()
    {
        //ショップシーンへ移行
        SceneManager.LoadScene("CreditScene");
    }
}