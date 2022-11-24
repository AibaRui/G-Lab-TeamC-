using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("�Q�[���I�[�o�[�̃p�l��")]
    [Tooltip("�Q�[���I�[�o�[�̃p�l��")] [SerializeField] GameObject _gameOverPanel;

    [Header("�Q�[���N���A�̃p�l��")]
    [Tooltip("�Q�[���N���A�̃p�l��")] [SerializeField] GameObject _gameClearPanel;


    [SerializeField] PauseManager _pauseManager;


    void Start()
    {
        _pauseManager = _pauseManager.GetComponent<PauseManager>();
    }


    void Update()
    {

    }



    public void GameOver()
    {
        //�p�l�����o��
        _gameOverPanel.SetActive(true);
        //�S�̂̃I�u�W�F�N�g�̓������~�߂�
        _pauseManager.PauseResumeGameOver();
    }

    public void GameClear()
    {
        //�p�l�����o��
        _gameClearPanel.SetActive(true);
        //�S�̂̃I�u�W�F�N�g�̓������~�߂�
        _pauseManager.PauseResumeGameOver();
    }

}
