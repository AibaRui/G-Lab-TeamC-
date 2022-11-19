using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ギミックに継承させる</summary>
public abstract class GimickBase : MonoBehaviour
{
    PauseManager _pauseManager = default;
    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

   

    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= LevelUpPauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    void LevelUpPauseResume(bool isPause)
    {
        if (isPause)
        {
            GameOverPause();
        }
    }

    /// <summary>ゲームオーバー時に呼ぶ。アニメーションの停止、Rigidbodyの停止、判定消し</summary>
    public abstract void GameOverPause();

    /// <summary>一時停止時に呼ぶ。アニメーションの停止、Rigidbodyの停止、判定消しの処理を書く</summary>
    public abstract void Pause();
    /// <summary>ゲーム再開時に呼ぶ。アニメーションの再開、Rigidbodyの再開、判定再開を書く</summary>
    public abstract void Resume();
}
