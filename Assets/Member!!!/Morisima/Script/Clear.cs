using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{

    private AudioSource ItemAudio;

    // ���ʉ��Đ��t���O
    public static bool Itemflag = false;
    
    // �A�C�e���擾��
    [SerializeField]
    private AudioClip ItemGetSE;
    
    void Start()
    {
        // �I�[�f�B�I�擾
        ItemAudio= GetComponent<AudioSource>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    // Enter �ŃN���A��ʂֈڍs
        //    SceneManager.LoadScene("ClearScene");
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Space �ŃQ�[���I�[�o�[��ʂֈڍs
        //    SceneManager.LoadScene("GameOverScene");
        //}

        // true�Ō��ʉ��Đ�
        if(Itemflag != false) { ItemAudio.PlayOneShot(ItemGetSE); Itemflag = false; }
    }
}
