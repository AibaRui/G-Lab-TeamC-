using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{

    private AudioSource ItemAudio;

    // 効果音再生フラグ
    public static bool Itemflag = false;
    
    // アイテム取得音
    [SerializeField]
    private AudioClip ItemGetSE;
    
    void Start()
    {
        // オーディオ取得
        ItemAudio= GetComponent<AudioSource>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    // Enter でクリア画面へ移行
        //    SceneManager.LoadScene("ClearScene");
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Space でゲームオーバー画面へ移行
        //    SceneManager.LoadScene("GameOverScene");
        //}

        // trueで効果音再生
        if(Itemflag != false) { ItemAudio.PlayOneShot(ItemGetSE); Itemflag = false; }
    }
}
