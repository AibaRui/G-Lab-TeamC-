using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;
using static Cinemachine.DocumentationSortingAttribute;
using System.Data;

public class Ranking : MonoBehaviour
{
    public static Ranking instance;
    [SerializeField]
     Button btn;


    private float time = 0;
    private int point = 0;
    private string alltime = null;
    public int Point { get => point;}
    public float Time { get => time;}
    string ranking_gg = "  けんがい";

     string[] ranking = { "ランキング1", "ランキング2", "ランキング3", "ランキング4", "ランキング5" };
    string[] ranking1 = { "ranking1", "ranking2", "ranking3", "ranking4", "ranking5" };
    string[] ranking2 = { "ranking1", "ranking2", "ranking3", "ranking4", "ranking5" };
    
    public string[] secondValue = new string[5];
    public float[] timeValue = new float[5];
    public int[] rankingValue = new int[5];

   
    string[] player = new string[5];
    

    [SerializeField, 
     Header("表示させるテキスト")]
    private TextMeshProUGUI[] rankingText = new TextMeshProUGUI[5];
    [SerializeField]
    TextMeshProUGUI[] rankingName = new TextMeshProUGUI[5];
    [SerializeField]
    TextMeshProUGUI[] rankingTime = new TextMeshProUGUI[5];
    [SerializeField] 
    TextMeshProUGUI ranking_Now1 = null;
    [SerializeField]
    TextMeshProUGUI ranking_Now2 = null;
    [SerializeField]
    TextMeshProUGUI time_Now = null;


    // Use this for initialization
    void Start()
    {
        point += GameClearScript.ScorePoint;
        alltime = StopWatchScript.Second.ToString();
        time = StopWatchScript.second_;
        ranking_Now1.text = Point.ToString();
        time_Now.text = alltime.ToString();


        GameOver.image_Score1[0] = 4;
        GameOver.image_Score1[1] = 4;
        GameOver.image_Score1[2] = 4;
        GameOver.image_Score1[3] = 0;
        GameOver.image_Score1[4] = 0;
        GameOver.image_Score1[5] = 0;
       
        btn = GetComponent<Button>();
        GetRanking();

  
         if (Point  > rankingValue[4] )
        {
            btn.interactable = true;
            
        }
        else if (Point < rankingValue[4])
        {
            btn.interactable = false;
             if (Time > timeValue[4])
            {
                btn.interactable = false;
            }
        }
        else if(Point == rankingValue[4]  )
        {
            if (Time < timeValue[4])
            {
                btn.interactable = true;
            }
            
        }
        

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
            rankingName[i].text = player[i].ToString();
            if (rankingValue[0] < Point )
            {
                ranking_Now2.text = ranking[0];
            }
            else if(rankingValue[0] == Point && timeValue[0] > Time)
            {
                ranking_Now2.text = ranking[0];
            }
            else if (rankingValue[1] < Point )
            {
                ranking_Now2.text = ranking[1];
            }
            else if (rankingValue[1] == Point && timeValue[1] > Time)
            {
                ranking_Now2.text = ranking[1];
            }
            else if (rankingValue[2] < Point)
            {
                ranking_Now2.text = ranking[2];
            }
            else if (rankingValue[2] == Point && timeValue[2] > Time)
            {
                ranking_Now2.text = ranking[2];
            }
            else if (rankingValue[3] < Point )
            {
                ranking_Now2.text = ranking[3];
            }
            else if (rankingValue[3] == Point && timeValue[3] > Time)
            {
                ranking_Now2.text = ranking[3];
            }
            else if (rankingValue[4] < Point )
            {
                ranking_Now2.text = ranking[4];
            }
            else if (rankingValue[4] == Point && timeValue[4] > Time)
            {
                ranking_Now2.text = ranking[4];
            }
            else if (rankingValue[4] == Point && timeValue[4] == Time)
            {

                ranking_Now2.text = ranking_gg;

            }
            else if (rankingValue[0] == Point || timeValue[0] == Time)
            {

                ranking_Now2.text = ranking[1];

            }
            else if (rankingValue[1] == Point || timeValue[1] == Time)
            {

                ranking_Now2.text = ranking[2];

            }
            else if (rankingValue[2] == Point || timeValue[2] == Time)
            {

                ranking_Now2.text = ranking[3];

            }
            else if (rankingValue[3] == Point || timeValue[3] == Time)
            {
                ranking_Now2.text = ranking[4];

            }
            else { ranking_Now2.text = ranking_gg; }




            for (int a = 0; a < rankingTime.Length; a++)
            {
                rankingTime[a].text = secondValue[a].ToString();
            }




            //PlayerPrefs.DeleteAll();

        }
    }
    public void OnClick()
    {
        
        GameObject objCanvas = GameObject.Find("Canvas");
        GameObject objInputField = objCanvas.transform.Find("InputField").gameObject;
        GameObject objInputFieldText = objInputField.transform.Find("Text").gameObject;
        TextMeshProUGUI inputFieldText = objInputFieldText.GetComponent<TextMeshProUGUI>();

        if (Point > rankingValue[0]  )
        {
            var namebox = player[0].ToString();
            player[0] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = player[1].ToString();
            player[1] = namebox.ToString();
            var timebox = secondValue[0].ToString();
            secondValue[0] = alltime.ToString();
            secondValue[4] = secondValue[3].ToString();
            secondValue[3] = secondValue[2].ToString();
            secondValue[2] = secondValue[1].ToString();
            secondValue[1] = timebox.ToString();



        }
        else if(Point == rankingValue[0] && timeValue[0] > Time)
        {
            var namebox = player[0].ToString();
            player[0] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = player[1].ToString();
            player[1] = namebox.ToString();
            var timebox = secondValue[0].ToString();
            secondValue[0] = alltime.ToString();
            secondValue[4] = secondValue[3].ToString();
            secondValue[3] = secondValue[2].ToString();
            secondValue[2] = secondValue[1].ToString();
            secondValue[1] = timebox.ToString();
        }
        else if(Point > rankingValue[1])
        {
            var namebox = player[1].ToString();
            player[1] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = namebox.ToString();
            var timebox = secondValue[1].ToString();
            secondValue[1] = alltime.ToString();
            secondValue[4] = secondValue[3].ToString();
            secondValue[3] = secondValue[2].ToString();
            secondValue[2] = timebox.ToString();
        }
        else if(Point == rankingValue[1] && timeValue[1] > Time)
        {
            var namebox = player[1].ToString();
            player[1] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = namebox.ToString();
            var timebox = secondValue[1].ToString();
            secondValue[1] = alltime.ToString();
            secondValue[4] = secondValue[3].ToString();
            secondValue[3] = secondValue[2].ToString();
            secondValue[2] = timebox.ToString();
        }
        else if(Point > rankingValue[2] )
        {
            var namebox = player[2].ToString();
            player[2] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = namebox.ToString();
            var timebox = secondValue[2].ToString();
            secondValue[2] = alltime.ToString();
            secondValue[4] = secondValue[3].ToString();
            secondValue[3] = timebox.ToString();
        }
        else if(Point == rankingValue[2] && timeValue[2] > Time)
        {
            var namebox = player[2].ToString();
            player[2] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = namebox.ToString();
            var timebox = secondValue[2].ToString();
            secondValue[2] = alltime.ToString();
            secondValue[4] = secondValue[3].ToString();
            secondValue[3] = timebox.ToString();
        }
        else if(Point > rankingValue[3] )
        {
            var namebox = player[3].ToString();
            player[3] = inputFieldText.text.ToString();
            player[4] = namebox.ToString();
            var timebox = secondValue[3].ToString();
            secondValue[3] = alltime.ToString();
            secondValue[4] = timebox.ToString();

        }
        else if(Point == rankingValue[3] && timeValue[3] > Time)
        {
            var namebox = player[3].ToString();
            player[3] = inputFieldText.text.ToString();
            player[4] = namebox.ToString();
            var timebox = secondValue[3].ToString();
            secondValue[3] = alltime.ToString();
            secondValue[4] = timebox.ToString();
        }
        else if (Point > rankingValue[4] )
        {
            player[4] = inputFieldText.text.ToString();
            secondValue[4] = alltime.ToString();
        }
        else if(Point == rankingValue[4] && timeValue[4] > Time)
        {
            player[4] = inputFieldText.text.ToString();
            secondValue[4] = alltime.ToString();
        }
        


      
        SetRanking(point,time);
       
        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
            rankingName[i].text = player[i].ToString();
        }
        for(int a = 0; a < rankingTime.Length; a++)
        {
            rankingTime[a].text = secondValue[a].ToString();
        }



  
        btn.interactable = false;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    public void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
            player[0] = PlayerPrefs.GetString("プレイヤー");
            player[1] = PlayerPrefs.GetString("プレイヤー1");
            player[2] = PlayerPrefs.GetString("プレイヤー2");
            player[3] = PlayerPrefs.GetString("プレイヤー3");
            player[4] = PlayerPrefs.GetString("プレイヤー4");
            secondValue[0] = PlayerPrefs.GetString("プレイヤー5");
            secondValue[1] = PlayerPrefs.GetString("プレイヤー6");
            secondValue[2] = PlayerPrefs.GetString("プレイヤー7");
            secondValue[3] = PlayerPrefs.GetString("プレイヤー8");
            secondValue[4] = PlayerPrefs.GetString("プレイヤー9");
            

        }
        for( int a = 0; a < ranking1.Length; a++)
        {
            timeValue[a] = PlayerPrefs.GetFloat(ranking1[a]);
        }
        
   
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>
    public void SetRanking(int _value, float _value1)
    {
       
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            
            //取得した値とRankingの値を比較して入れ替え
            
           
            if (_value > rankingValue[i] && _value != rankingValue[i])
            {

                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                var change1 = timeValue[i];
                timeValue[i] = _value1;
                _value1 = change1;
                
            }
            else if (_value == rankingValue[i])
            {
                if (_value1 < timeValue[i])
                {
                    var change = rankingValue[i];
                    rankingValue[i] = _value;
                    _value = change;
                    var change1 = timeValue[i];
                    timeValue[i] = _value1;
                    _value1 = change1;
                  /*  var change2 = secondValue[i];
                    secondValue[i] = _value2;
                    _value2 = change2;*/
              
                }
            }
        }
  
        

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
            PlayerPrefs.SetString("プレイヤー", player[0]);
            PlayerPrefs.SetString("プレイヤー1", player[1]);
            PlayerPrefs.SetString("プレイヤー2", player[2]);
            PlayerPrefs.SetString("プレイヤー3", player[3]);
            PlayerPrefs.SetString("プレイヤー4", player[4]);

            PlayerPrefs.SetString("プレイヤー5", secondValue[0]);
            PlayerPrefs.SetString("プレイヤー6", secondValue[1]);
            PlayerPrefs.SetString("プレイヤー7", secondValue[2]);
            PlayerPrefs.SetString("プレイヤー8", secondValue[3]);
            PlayerPrefs.SetString("プレイヤー9", secondValue[4]);
            
            

        }
        for(int a = 0; a < ranking1.Length; a++)
        {
            PlayerPrefs.SetFloat(ranking1[a], timeValue[a]);
        }
        PlayerPrefs.Save();


    }
 
}