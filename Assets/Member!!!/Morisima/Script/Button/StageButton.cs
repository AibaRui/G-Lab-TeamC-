using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public void Onclick()
    {
        //�X�e�[�W�I���V�[���ֈڍs
        SceneManager.LoadScene("StageScene");
    }
}