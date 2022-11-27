using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _gameOverPanel;

    [SerializeField] GameObject _gameClearPanel;

    PauseManager _pause;

    private void Awake()
    {
        _pause = FindObjectOfType<PauseManager>();
    }


    public void GameClear()
    {
        _gameClearPanel.SetActive(true);
        _pause.PauseResumeGameEnd();
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _pause.PauseResumeGameEnd();
    }

}
