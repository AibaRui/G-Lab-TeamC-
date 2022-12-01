using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    public Button btn;

  

    int point = 5;

    string[] ranking = { "�����L���O1��", "�����L���O2��", "�����L���O3��", "�����L���O4��", "�����L���O5��" };
    int[] rankingValue = new int[5];

    [SerializeField, Header("�\��������e�L�X�g")]
     TextMeshProUGUI[] rankingText = new TextMeshProUGUI[5];

    // Use this for initialization
    void Start()
    {
        btn = GetComponent<Button>();
        GetRanking();
        if (point <= rankingValue[4]) {
            btn.interactable = false;
           
        }
        else { btn.interactable = true;}

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
        if (objCanvas.transform.Find("InputField").gameObject == null)
        {
            Debug.Log("NULL");

        }
        else { Debug.Log(inputFieldText.text); }


        SetRanking(point);
        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
        }

        btn.interactable = false;
    }




    /// <summary>
    /// �����L���O�Ăяo��
    /// </summary>
    void GetRanking()
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
    void SetRanking(int _value)
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