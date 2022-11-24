using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("ゲームオーバーのパネル")]
    [Tooltip("ゲームオーバーのパネル")] [SerializeField] GameObject _gameOverPanel;

    [Header("ゲームクリアのパネル")]
    [Tooltip("ゲームクリアのパネル")] [SerializeField] GameObject _gameClearPanel;


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
        //パネルを出す
        _gameOverPanel.SetActive(true);
        //全体のオブジェクトの動きを止める
        _pauseManager.PauseResumeGameOver();
    }

    public void GameClear()
    {
        //パネルを出す
        _gameClearPanel.SetActive(true);
        //全体のオブジェクトの動きを止める
        _pauseManager.PauseResumeGameOver();
    }

}
