using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowtoplayButton : MonoBehaviour
{
    public void Onclick()
    {
        //�V�ѕ��V�[���ֈڍs
        SceneManager.LoadScene("HowtoplayScene");
    }
}