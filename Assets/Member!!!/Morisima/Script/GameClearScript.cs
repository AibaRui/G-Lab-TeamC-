using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearScript : MonoBehaviour
{
    // Ornament æ“¾”Ši”[•Ï”
    private int Score1, Score2, Score3;

    // Ornament ImageŠi”[•Ï”
    [SerializeField]
    private Image[] Ornament1Image;

    private bool GamePoint = false;

    //private Sprite[] Image1;

    public void Start()
    {
        Score1 = Ornament.GetOrnament1();
        Score2 = Ornament.GetOrnament2();
        Score3 = Ornament.GetOrnament3();
    }

    private void Update()
    {
        
    }

    void Judge()
    {
        if(Score1 == 1)
        {

        }
    }

}