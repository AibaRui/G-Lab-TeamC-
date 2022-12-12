using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearScript : MonoBehaviour
{
    // Ornament Image格納変数
    [SerializeField]
    private Image[] Ornament1Image;
    [SerializeField]
    private Image[] Ornament2Image;
    [SerializeField]
    private Image[] Ornament3Image;

    private void Update()
    {
        Judge();
    }

    void Judge()
    {
        // Ornament1 取得数に応じたオーナメントの表示
        if (Ornament1.Ornament1Score >= 1) { Ornament1Image[0].gameObject.SetActive(true); }

        // Ornament2 取得数に応じたオーナメントの表示
        if (Ornament2.Ornament2Score >= 1) { Ornament2Image[0].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 2) { Ornament2Image[1].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 3) { Ornament2Image[2].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 4) { Ornament2Image[3].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 5) { Ornament2Image[4].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 6) { Ornament2Image[5].gameObject.SetActive(true); }
        if (Ornament2.Ornament2Score >= 7) { Ornament2Image[6].gameObject.SetActive(true); }

        // Ornament3 取得数に応じたオーナメントの表示
        if (Ornament3.Ornament3Score >= 1) { Ornament3Image[0].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 2) { Ornament3Image[1].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 3) { Ornament3Image[2].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 4) { Ornament3Image[3].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 5) { Ornament3Image[4].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 6) { Ornament3Image[5].gameObject.SetActive(true); }
        if (Ornament3.Ornament3Score >= 7) { Ornament3Image[6].gameObject.SetActive(true); }
    }

}