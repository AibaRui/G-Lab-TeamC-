using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Button : MonoBehaviour
{
    public void Onclick()
    {
        //�X�e�[�W1�ֈڍs
        SceneManager.LoadScene("PlayScene");
    }
}