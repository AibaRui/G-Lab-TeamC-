using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearScript : MonoBehaviour
{

    int Score1, Score2, Score3;
    bool Image1, Image2, Image3;

    public void Start()
    {
        Score1 = Ornament.GetOrnament1();
        Score2 = Ornament.GetOrnament2();
        Score3 = Ornament.GetOrnament3();
    }

    private void Update()
    {
       
    }
}