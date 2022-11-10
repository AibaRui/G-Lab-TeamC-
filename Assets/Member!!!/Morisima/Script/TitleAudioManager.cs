using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAudioManager : MonoBehaviour
{
    
    private static bool AudioPlay = false;

    public void Start()
    {
        Awake();
    }

    private void Awake()
    {
        AudioPlay = true;
        DontDestroyOnLoad(this.gameObject);

        if (AudioPlay == false)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
