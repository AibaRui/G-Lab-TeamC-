using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking_Read : MonoBehaviour
{
  

    [SerializeField]
     TextMeshProUGUI[] rankingText1 = new TextMeshProUGUI[5];
    [SerializeField]
    TextMeshProUGUI[] Player_Name = new TextMeshProUGUI[5];
    [SerializeField]
    TextMeshProUGUI[] ranking_Time = new TextMeshProUGUI[5];
        
    string[] player_ = new string[5];
   
    // Start is called before the first frame update
    void Start()
    {
        
        Ranking.instance.GetRanking();
        player_[0] = PlayerPrefs.GetString("プレイヤー");
        player_[1] = PlayerPrefs.GetString("プレイヤー1");
        player_[2] = PlayerPrefs.GetString("プレイヤー2");
        player_[3] = PlayerPrefs.GetString("プレイヤー3");
        player_[4] = PlayerPrefs.GetString("プレイヤー4");
        for (int i = 0; i < rankingText1.Length; i++)
        {
            ranking_Time[i].text = Ranking.instance.timeValue[i].ToString();
            rankingText1[i].text = Ranking.instance.rankingValue[i].ToString();
        }
        Player_Name[0].text = player_[0].ToString();
        Player_Name[1].text = player_[1].ToString();
        Player_Name[2].text = player_[2].ToString();
        Player_Name[3].text = player_[3].ToString();
        Player_Name[4].text = player_[4].ToString();

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
