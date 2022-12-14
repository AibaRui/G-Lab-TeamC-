using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public bool _isPause = false;
    /// <summary>true �̎��͈ꎞ��~�Ƃ���</summary>
    bool _pauseFlg = false;
    /// <summary>�ꎞ��~�E�ĊJ�𐧌䂷��֐��̌^�i�f���Q�[�g�j���`����</summary>
    public delegate void Pause(bool isPause);
    /// <summary>�f���Q�[�g�����Ă����ϐ�</summary>
    Pause _onPauseResume = default;



    public bool _isGameOver = false;
    /// <summary>true �̎��͈ꎞ��~�Ƃ���</summary>
    bool _gameOverFlg = false;
    /// <summary>�ꎞ��~�E�ĊJ�𐧌䂷��֐��̌^�i�f���Q�[�g�j���`����</summary>
    public delegate void GamerOver(bool isPause);
    /// <summary>�f���Q�[�g�����Ă����ϐ�</summary>
    GamerOver _onGameOverUp = default;




    /// <summary>�ꎞ��~�E�ĊJ������f���Q�[�g�v���p�e�B</summary>
    public GamerOver GameEnd
    {
        get { return _onGameOverUp; }
        set { _onGameOverUp = value; }
    }


    /// <summary>�ꎞ��~�E�ĊJ������f���Q�[�g�v���p�e�B</summary>
    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    void Update()
    {
        // ESC �L�[�������ꂽ��ꎞ��~�E�ĊJ��؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            PauseResume();
        }



    }

    /// <summary>�ꎞ��~�E�ĊJ��؂�ւ���</summary>
    void PauseResume()
    {
        _pauseFlg = !_pauseFlg;
        _isPause = !_isPause;

        if (_onPauseResume != null)
        {
            _onPauseResume(_pauseFlg);  // ����ŕϐ��ɑ�������֐����i�S�āj�Ăяo����
        }

    }

   public void PauseResumeGameEnd()
    {
        _gameOverFlg = !_gameOverFlg;
        _isGameOver = !_isGameOver;

        if (_onGameOverUp != null)
        {
            _onGameOverUp(_gameOverFlg);  // ����ŕϐ��ɑ�������֐����i�S�āj�Ăяo����
        }

    }
}