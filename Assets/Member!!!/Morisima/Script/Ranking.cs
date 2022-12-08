using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;
using static Cinemachine.DocumentationSortingAttribute;

public class Ranking : MonoBehaviour
{
    public static Ranking instance;
    [SerializeField]
     Button btn;


    private int a = 3;
    private int point = 60;
    public int Point { get => point;}
    string ranking_gg = "  ���񂪂�";

    string[] ranking = { "�����L���O1", "�����L���O2", "�����L���O3", "�����L���O4", "�����L���O5" };
     
    public int[] rankingValue = new int[5];
     string[] playerValue = new string[5];
    string[] player = new string[5];
    

    [SerializeField, 
     Header("�\��������e�L�X�g")]
    private TextMeshProUGUI[] rankingText = new TextMeshProUGUI[5];
    [SerializeField] 
    TextMeshProUGUI ranking_Now1 = null;
    [SerializeField]
    TextMeshProUGUI ranking_Now2 = null;
    [SerializeField]
     TextMeshProUGUI[] ranking_name = new TextMeshProUGUI[5];

    // Use this for initialization
    void Start()
    {
        ranking_Now1.text = Point.ToString();
     /*   ranking_name[0].text= player[0].ToString();
        ranking_name[1].text= player[1].ToString();
        ranking_name[2].text= player[2].ToString();
        ranking_name[3].text= player[3].ToString();
        ranking_name[4].text= player[4].ToString();*/
          
        
       
        btn = GetComponent<Button>();
        GetRanking();
        
        if (Point <= rankingValue[4])
        {
            btn.interactable = false;

        }
        else
        {
            btn.interactable = true;
        }

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
            ranking_name[i].text = player[i].ToString();
            if (rankingValue[0] < Point)
            {
                ranking_Now2.text = ranking[0];
            }
            else if(rankingValue[1] < Point)
            {
                ranking_Now2.text = ranking[1];
            }
            else if (rankingValue[2] < Point)
            {
                ranking_Now2.text = ranking[2];
            }
            else if(rankingValue[3] < Point)
            {
                ranking_Now2.text = ranking[3];
            }
            else if (rankingValue[4] < Point)
            {
                ranking_Now2.text = ranking[4];
            }
            else { ranking_Now2.text = ranking_gg; }
        }


       

        //PlayerPrefs.DeleteAll();

    }
    
    public void OnClick()
    {
        GameObject objCanvas = GameObject.Find("Canvas");
        GameObject objInputField = objCanvas.transform.Find("InputField").gameObject;
        GameObject objInputFieldText = objInputField.transform.Find("Text").gameObject;
        TextMeshProUGUI inputFieldText = objInputFieldText.GetComponent<TextMeshProUGUI>();

        if (Point > rankingValue[0])
        {
            var namebox = player[0].ToString();
            player[0] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = player[1].ToString();
            player[1] = namebox.ToString();


        }
        else if(Point > rankingValue[1])
        {
            var namebox = player[1].ToString();
            player[1] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = player[2].ToString();
            player[2] = namebox.ToString();
        }
        else if(Point > rankingValue[2])
        {
            var namebox = player[2].ToString();
            player[2] = inputFieldText.text.ToString();
            player[4] = player[3].ToString();
            player[3] = namebox.ToString();
        }
        else if(Point > rankingValue[3])
        {
            var namebox = player[3].ToString();
            player[3] = inputFieldText.text.ToString();
            player[4] = namebox.ToString();

        }
        else if (Point > rankingValue[4])
        {
            player[4] = inputFieldText.text.ToString();
        }
     /*   else if (Point > rankingValue[1])
        {
            ranking_name[1].text = inputFieldText.text.ToString();
        }
*/
        SetRanking(point);
       
        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
            ranking_name[i].text = player[i].ToString();
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
        
   
    }
    /// <summary>
    /// �����L���O��������
    /// </summary>
    public void SetRanking(int _value)
    {
       
        //�������ݗp
        for (int i = 0; i < ranking.Length; i++)
        {
            
            //�擾�����l��Ranking�̒l���r���ē���ւ�
            if (_value > rankingValue[i])
            {
                
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
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



    }
 
}