using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Forme : MonoBehaviour
{
    //InputField���i�[���邽�߂̕ϐ�
    InputField InputField;


    // Start is called before the first frame update
    void Start()
    {
        //InputField�R���|�[�l���g���擾
        InputField = GameObject.Find("InputField").GetComponent<InputField>();
    }


    //���͂��ꂽ���O����ǂݎ���ăR���\�[���ɏo�͂���֐�
    public void OnClick()
    {
        //InputField����e�L�X�g�����擾����
        string name = InputField.text;
        Debug.Log(name);

        //���̓t�H�[���̃e�L�X�g����ɂ���
        InputField.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    


}
