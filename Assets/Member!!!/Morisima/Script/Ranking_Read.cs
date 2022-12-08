using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking_Read : MonoBehaviour
{
  

    [SerializeField]
     TextMeshProUGUI[] rankingText1 = new TextMeshProUGUI[5];
   
    // Start is called before the first frame update
    void Start()
    {
        Ranking.instance.GetRanking();
       
        for (int i = 0; i < rankingText1.Length; i++)
        {

            rankingText1[i].text = Ranking.instance.rankingValue[i].ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
