using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Button : MonoBehaviour
{
    public void Onclick()
    {
        //�X�e�[�W2�ֈڍs
        SceneManager.LoadScene("PlayScene");
    }
}