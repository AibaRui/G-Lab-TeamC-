using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("ゲームオーバーのパネル")]
    [SerializeField] GameObject _gameOverPanel;

    [Header("クリアシーンの名前")]
    [SerializeField] string _clearSceneName;

    [SerializeField] List<AudioSource> _stopMusicOnGameEnd = new List<AudioSource>();


    PauseManager _pause;

    private void Awake()
    {
        _pause = FindObjectOfType<PauseManager>();
    }


    public void GameClear()
    {
        SceneManager.LoadScene(_clearSceneName);
        _pause.PauseResumeGameEnd();

        _stopMusicOnGameEnd.ForEach(i => i.Stop());
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _pause.PauseResumeGameEnd();
        _stopMusicOnGameEnd.ForEach(i => i.Stop());
    }

}
