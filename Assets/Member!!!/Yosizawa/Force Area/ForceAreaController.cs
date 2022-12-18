using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]

class ForceAreaController : GimickBase
{
    /// <summary>—Í‚ð‰Á‚¦‚é”ÍˆÍ</summary>
    private BoxCollider2D _boxCol = null;
    /// <summary>—Í‚ð‰Á‚¦‚é”ÍˆÍ‚É“ü‚Á‚Ä‚¢‚é‚Æ‚«‚ÌSE</summary>
    private AudioSource _audio = null;

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider2D>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audio.DOFade(1f, 0f);
        _audio.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _audio.DOFade(0f, 1f).OnComplete(() => _audio.Stop());
    }

    public override void GameOverPause()
    {
        _boxCol.enabled = false;
    }

    public override void Pause()
    {
        _boxCol.enabled = false;
    }

    public override void Resume()
    {
        _boxCol.enabled = true;
    }
}
