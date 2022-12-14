using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClearScript : MonoBehaviour
{
    // Ornament Image�i�[�ϐ�
    [SerializeField]
    private Image[] OrnamentImage;

    // ����A�C�e���̐����e�L�X�g�ɔ��f
    [SerializeField]
    TextMeshProUGUI ScoreText = null;

    // Ornament �擾���̍��v�l�i�[�ϐ�
    public static int ScorePoint = 0;

    private void Start()
    {
        ScorePoint= 0;
    }

    private void Update()
    {
        ScoreText.text = ScorePoint.ToString();
        Judge();
    }

    // Ornament �擾�Ǘ��֐�
    void Judge()
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
        if (Ornament5.Ornament5Score >= 3) { OrnamentImage[9].gameObject.SetActive(true); }

        // Ornament6 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament6.Ornament6Score >= 1) { OrnamentImage[10].gameObject.SetActive(true); }
        if (Ornament6.Ornament6Score >= 2) { OrnamentImage[11].gameObject.SetActive(true); }
        if (Ornament6.Ornament6Score >= 3) { OrnamentImage[12].gameObject.SetActive(true); }

        // Ornament7 �擾���ɉ������I�[�i�����g�̕\��
        if (Ornament7.Ornament7Score >= 1) { OrnamentImage[13].gameObject.SetActive(true); }
        if (Ornament7.Ornament7Score >= 2) { OrnamentImage[14].gameObject.SetActive(true); }

        // Ornament �擾���̍��v�l
        ScorePoint = Ornament1.Ornament1Score + Ornament2.Ornament2Score
                   + Ornament3.Ornament3Score + Ornament4.Ornament4Score
                   + Ornament5.Ornament5Score + Ornament6.Ornament6Score
                   + Ornament7.Ornament7Score;
    }

}