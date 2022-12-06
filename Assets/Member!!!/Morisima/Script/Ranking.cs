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


    private int a = 1;
    private int point = 10;
    public int Point { get => point;}
    string ranking_gg = "kengai";

    string[] ranking = { "�����L���O1��", "�����L���O2��", "�����L���O3��", "�����L���O4��", "�����L���O5��" };
    public int[] rankingValue = new int[5];

    [SerializeField, 
     Header("�\��������e�L�X�g")]
    private TextMeshProUGUI[] rankingText = new TextMeshProUGUI[5];
    [SerializeField] 
    TextMeshProUGUI ranking_Now1 = null;
    [SerializeField]
    TextMeshProUGUI ranking_Now2 = null;

    // Use this for initialization
    void Start()
    {
        ranking_Now1.text = Point.ToString();
   
          
        
       
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

       // PlayerPrefs.DeleteAll();
       
    }
    
    public void OnClick()
    {
        GameObject objCanvas = GameObject.Find("Canvas");
        GameObject objInputField = objCanvas.transform.Find("InputField").gameObject;
        GameObject objInputFieldText = objInputField.transform.Find("Text").gameObject;
        TextMeshProUGUI inputFieldText = objInputFieldText.GetComponent<TextMeshProUGUI>();
        if (string.IsNullOrWhiteSpace(inputFieldText.text))
        {
            Debug.Log("Null");
        }
        else
        {
           Debug.Log(inputFieldText.text);
        }

        SetRanking(point);
        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
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
        }
    }
 
}