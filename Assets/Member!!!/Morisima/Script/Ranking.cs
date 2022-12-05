using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;
using static Cinemachine.DocumentationSortingAttribute;

public class Ranking : MonoBehaviour
{
    public static Ranking instance;
    [SerializeField]
    public Button btn;

    

    int point = 5;

    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    public int[] rankingValue = new int[5];

    [SerializeField, Header("表示させるテキスト")]
    public TextMeshProUGUI[] rankingText = new TextMeshProUGUI[5];
    public TextMeshProUGUI ranking_Now = null;

    // Use this for initialization
    void Start()
    {
        ranking_Now.text = point.ToString();
        btn = GetComponent<Button>();
        GetRanking();
        if (point <= rankingValue[4])
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
    /// ランキング呼び出し
    /// </summary>
    public void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>
    public void SetRanking(int _value)
    {
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
 
}