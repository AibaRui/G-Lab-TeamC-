using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public bool _isPause = false;
    /// <summary>true の時は一時停止とする</summary>
    bool _pauseFlg = false;
    /// <summary>一時停止・再開を制御する関数の型（デリゲート）を定義する</summary>
    public delegate void Pause(bool isPause);
    /// <summary>デリゲートを入れておく変数</summary>
    Pause _onPauseResume = default;



    public bool _isGameOver = false;
    /// <summary>true の時は一時停止とする</summary>
    bool _gameOverFlg = false;
    /// <summary>一時停止・再開を制御する関数の型（デリゲート）を定義する</summary>
    public delegate void GamerOver(bool isPause);
    /// <summary>デリゲートを入れておく変数</summary>
    GamerOver _onGameOverUp = default;




    /// <summary>一時停止・再開を入れるデリゲートプロパティ</summary>
    public GamerOver GameEnd
    {
        get { return _onGameOverUp; }
        set { _onGameOverUp = value; }
    }


    /// <summary>一時停止・再開を入れるデリゲートプロパティ</summary>
    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    void Update()
    {
        // ESC キーが押されたら一時停止・再開を切り替える
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            PauseResume();
        }



    }

    /// <summary>一時停止・再開を切り替える</summary>
    void PauseResume()
    {
        _pauseFlg = !_pauseFlg;
        _isPause = !_isPause;

        if (_onPauseResume != null)
        {
            _onPauseResume(_pauseFlg);  // これで変数に代入した関数を（全て）呼び出せる
        }

    }

   public void PauseResumeGameEnd()
    {
        _gameOverFlg = !_gameOverFlg;
        _isGameOver = !_isGameOver;

        if (_onGameOverUp != null)
        {
            _onGameOverUp(_gameOverFlg);  // これで変数に代入した関数を（全て）呼び出せる
        }

    }
}