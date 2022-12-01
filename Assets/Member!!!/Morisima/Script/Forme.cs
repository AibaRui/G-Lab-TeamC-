using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Forme : MonoBehaviour
{
    //InputFieldを格納するための変数
    InputField InputField;


    // Start is called before the first frame update
    void Start()
    {
        //InputFieldコンポーネントを取得
        InputField = GameObject.Find("InputField").GetComponent<InputField>();
    }


    //入力された名前情報を読み取ってコンソールに出力する関数
    public void OnClick()
    {
        //InputFieldからテキスト情報を取得する
        string name = InputField.text;
        Debug.Log(name);

        //入力フォームのテキストを空にする
        InputField.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    


}
