using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAudioManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //プレイ画面以外に遷移しても再生する処理
        DontDestroyOnLoad(this);

    }
}
