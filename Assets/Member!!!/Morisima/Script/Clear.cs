using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Enter �ŃN���A��ʂֈڍs
            SceneManager.LoadScene("ClearScene");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Space �ŃQ�[���I�[�o�[��ʂֈڍs
            SceneManager.LoadScene("GameOverScene");
        }

    }

    /*private void ItemSE()
    {
        // Ornament1 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament1.Ornament1Score >= 1) { OrnamentImage[0].gameObject.SetActive(true); }

        // Ornament2 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament2.Ornament2Score >= 1) { OrnamentImage[1].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 2) { OrnamentImage[2].gameObject.SetActive(true); }

        // Ornament3 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament3.Ornament3Score >= 1) { OrnamentImage[3].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 2) { OrnamentImage[4].gameObject.SetActive(true); }

        // Ornament4 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament4.Ornament4Score >= 1) { OrnamentImage[5].gameObject.SetActive(true); }
        if (Ornament4.Ornament4Score >= 2) { OrnamentImage[6].gameObject.SetActive(true); }

        // Ornament5 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament5.Ornament5Score >= 1) { OrnamentImage[7].gameObject.SetActive(true); }
        if (Ornament5.Ornament5Score >= 2) { OrnamentImage[8].gameObject.SetActive(true); }

        // Ornament6 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament6.Ornament6Score >= 1) { OrnamentImage[9].gameObject.SetActive(true); }
        if (Ornament6.Ornament6Score >= 2) { OrnamentImage[10].gameObject.SetActive(true); }

        // Ornament7 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament7.Ornament7Score >= 1) { OrnamentImage[11].gameObject.SetActive(true); }
        if (Ornament7.Ornament7Score >= 2) { OrnamentImage[12].gameObject.SetActive(true); }

        // Ornament8 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament8.Ornament8Score >= 1) { OrnamentImage[13].gameObject.SetActive(true); }
        if (Ornament8.Ornament8Score >= 2) { OrnamentImage[14].gameObject.SetActive(true); }
    }*/

}
