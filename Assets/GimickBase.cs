using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�M�~�b�N�Ɍp��������</summary>
public abstract class GimickBase : MonoBehaviour
{
    PauseManager _pauseManager = default;
    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

   

    ///////Parse����/////

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
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

    /// <summary>�Q�[���I�[�o�[���ɌĂԁB�A�j���[�V�����̒�~�ARigidbody�̒�~�A�������</summary>
    public abstract void GameOverPause();

    /// <summary>�ꎞ��~���ɌĂԁB�A�j���[�V�����̒�~�ARigidbody�̒�~�A��������̏���������</summary>
    public abstract void Pause();
    /// <summary>�Q�[���ĊJ���ɌĂԁB�A�j���[�V�����̍ĊJ�ARigidbody�̍ĊJ�A����ĊJ������</summary>
    public abstract void Resume();
}
