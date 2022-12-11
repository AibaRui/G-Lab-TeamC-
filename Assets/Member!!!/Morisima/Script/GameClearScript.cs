using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearScript : MonoBehaviour
{
    // Ornament 取得数格納変数
    //private static int Score1, Score2, Score3;

    // Ornament Image格納変数
    [SerializeField]
    private Image[] Ornament1Image;
    [SerializeField]
    private Image[] Ornament2Image;
    [SerializeField]
    private Image[] Ornament3Image;

    public void Start()
    {

    }

    private void Update()
    {
        Judge();
    }

    void Judge()
    {
        // Ornament1 取得数に応じたオーナメントの表示
        if (Ornament1.Ornament1Score >= 1) { Ornament1Image[0].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 2) { Ornament1Image[1].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 3) { Ornament1Image[2].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 4) { Ornament1Image[3].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 5) { Ornament1Image[4].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 6) { Ornament1Image[5].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 7) { Ornament1Image[6].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 8) { Ornament1Image[7].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 9) { Ornament1Image[8].gameObject.SetActive(true); }
        if (Ornament1.Ornament1Score >= 10) { Ornament1Image[9].gameObject.SetActive(true); }

        // Ornament2 取得数に応じたオーナメントの表示
        if (Ornament2.Ornament2Score >= 1) { Ornament2Image[0].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 2) { Ornament2Image[1].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 3) { Ornament2Image[2].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 4) { Ornament2Image[3].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 5) { Ornament2Image[4].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 6) { Ornament2Image[5].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 7) { Ornament2Image[6].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 8) { Ornament2Image[7].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 9) { Ornament2Image[8].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 10) { Ornament2Image[9].gameObject.SetActive(true); }

        // Ornament3 取得数に応じたオーナメントの表示
        if (Ornament3.Ornament3Score >= 1) { Ornament3Image[0].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 2) { Ornament3Image[1].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 3) { Ornament3Image[2].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 4) { Ornament3Image[3].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 5) { Ornament3Image[4].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 6) { Ornament3Image[5].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 7) { Ornament3Image[6].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 8) { Ornament3Image[7].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 9) { Ornament3Image[8].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 10) { Ornament3Image[9].gameObject.SetActive(true); }
    }

}