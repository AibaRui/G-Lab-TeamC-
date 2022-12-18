using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("�Q�[���I�[�o�[�̃p�l��")]
    [SerializeField] GameObject _gameOverPanel;

    [Header("�N���A�V�[���̖��O")]
    [SerializeField] string _clearSceneName;


    PauseManager _pause;

    private void Awake()
    {
        _pause = FindObjectOfType<PauseManager>();
    }


    public void GameClear()
    {
        SceneManager.LoadScene(_clearSceneName);
        _pause.PauseResumeGameEnd();
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _pause.PauseResumeGameEnd();
    }

}
