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


    private float time = -5.55f;
    private int point = 51;
    public int Point { get => point;}
    public float Time { get => time;}
    string ranking_gg = "  ���񂪂�";

     string[] ranking = { "�����L���O1", "�����L���O2", "�����L���O3", "�����L���O4", "�����L���O5" };
    string[] ranking1 = { "ranking1", "ranking2", "ranking3", "ranking4", "ranking5" };

    public float[] timeValue = new float[5];
    public int[] rankingValue = new int[5];

   
    string[] player = new string[5];
    

    [SerializeField, 
     Header("�\��������e�L�X�g")]
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
        ranking_Now1.text = Point.ToString();
        time_Now.text = Time.ToString();


        GameOver.image_Score1[0] = 4;
        GameOver.image_Score1[1] = 4;
        GameOver.image_Score1[2] = 4;
        GameOver.image_Score1[3] = 0;
        GameOver.image_Score1[4] = 0;
        GameOver.image_Score1[5] = 0;
        btn = GetComponent<Button>();
        GetRanking();

  
         if (Point  > rankingValue[4] || Time < timeValue[4])
        {
            btn.interactable = true;
            
        }
        else
        {
            btn.interactable = false;
        }

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
            rankingName[i].text = player[i].ToString();
            if (rankingValue[0] < Point || timeValue[0] > Time)
            {
                ranking_Now2.text = ranking[0];
            }
            else if (rankingValue[1] < Point || timeValue[1] > Time)
            {
                ranking_Now2.text = ranking[1];
            }
            else if (rankingValue[2] < Point || timeValue[2] > Time )
            {
                ranking_Now2.text = ranking[2];
            }
            else if (rankingValue[3] < Point || timeValue[3] > Time)
            {
                ranking_Now2.text = ranking[3];
            }
            else if (rankingValue[4] < Point || timeValue[4] > Time)
            {
                ranking_Now2.text = ranking[4];
            }
            else if (rankingValue[4] == Point || timeValue[4] == Time)
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
             
           
             
            
            for (int a = 0; a < rankingTime.Length; a++)
            {
                rankingTime[a].text = timeValue[a].ToString();
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

        if (Point > rankingValue[0] || timeValue[0] > Time)
        {
            var namebox = player[0].ToString();
            player[0] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = player[1].ToString();
            player[1] = namebox.ToString();


        }
        else if(Point > rankingValue[1] || timeValue[1] > Time)
        {
            var namebox = player[1].ToString();
            player[1] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = namebox.ToString();
        }
        else if(Point > rankingValue[2] || timeValue[2] > Time)
        {
            var namebox = player[2].ToString();
            player[2] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = namebox.ToString();
        }
        else if(Point > rankingValue[3] || timeValue[3] > Time)
        {
            var namebox = player[3].ToString();
            player[3] = inputFieldText.text.ToString();
            player[4] = namebox.ToString();

        }
        else if (Point > rankingValue[4] || timeValue[4] > Time)
        {
            player[4] = inputFieldText.text.ToString();
        }
        


        /* else if (Point == rankingValue[2])
         {
             if (Time < timeValue[2])
             {
                 var namebox = player[2].ToString();
                 player[2] = inputFieldText.text.ToString();
                 player[4] = player[3].ToString();
                 player[3] = namebox.ToString();
             }
             else if (Time == timeValue[2])
             {
                 var namebox = player[3].ToString();
                 player[3] = inputFieldText.text.ToString();
                 player[4] = namebox.ToString();
             }
         }
         else if (Point == rankingValue[3])
         {
             if (Time < timeValue[3])
             {
                 var namebox = player[3].ToString();
                 player[3] = inputFieldText.text.ToString();
                 player[4] = namebox.ToString();
             }
             else if (Time == timeValue[3])
             {
                 player[4] = inputFieldText.text.ToString();
             }
         }
         else if (Point == rankingValue[4])
         {
             if (Time < timeValue[4])
             {
                 player[4] = inputFieldText.text.ToString();
             }
             else if (Time == timeValue[4])
             {
                 inputFieldText.text = "";
             }
         }*/
        /*   else if (Point > rankingValue[1])
           {
               ranking_name[1].text = inputFieldText.text.ToString();
           }
   */
        SetRanking(point,time);
       
        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
            rankingName[i].text = player[i].ToString();
        }
        for(int a = 0; a < rankingTime.Length; a++)
        {
            rankingTime[a].text = timeValue[a].ToString();
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
    /// �����L���O�Ăяo��
    /// </summary>
    public void GetRanking()
    {
        //�����L���O�Ăяo��
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
            player[0] = PlayerPrefs.GetString("�v���C���[");
            player[1] = PlayerPrefs.GetString("�v���C���[1");
            player[2] = PlayerPrefs.GetString("�v���C���[2");
            player[3] = PlayerPrefs.GetString("�v���C���[3");
            player[4] = PlayerPrefs.GetString("�v���C���[4");

        }
        for( int a = 0; a < ranking1.Length; a++)
        {
            timeValue[a] = PlayerPrefs.GetFloat(ranking1[a]);
        }
        
   
    }
    /// <summary>
    /// �����L���O��������
    /// </summary>
    public void SetRanking(int _value, float _value1)
    {
       
        //�������ݗp
        for (int i = 0; i < ranking.Length; i++)
        {
            
            //�擾�����l��Ranking�̒l���r���ē���ւ�
            
           
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
                }
            }
        }
  
        

        //����ւ����l��ۑ�
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
            PlayerPrefs.SetString("�v���C���[", player[0]);
            PlayerPrefs.SetString("�v���C���[1", player[1]);
            PlayerPrefs.SetString("�v���C���[2", player[2]);
            PlayerPrefs.SetString("�v���C���[3", player[3]);
            PlayerPrefs.SetString("�v���C���[4", player[4]);
            

        }
        for(int a = 0; a < ranking1.Length; a++)
        {
            PlayerPrefs.SetFloat(ranking1[a], timeValue[a]);
        }
        PlayerPrefs.Save();


    }
 
}