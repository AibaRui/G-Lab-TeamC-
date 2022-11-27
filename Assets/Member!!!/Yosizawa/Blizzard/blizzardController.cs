using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class blizzardController : MonoBehaviour
{
    [SerializeField, Tooltip("blizzardを起こすために使うpanel")]
    private Image _blizzardPanel = default;
    [SerializeField, Range(1f, 10f), Tooltip("どのくらいの時間吹雪を起こすか")]
    private float _stormTime;
    [SerializeField, Range(0f, 1f), Tooltip("blizzardを起こすときに使うpanelのalpha値")]
    private float _alpha;
    /// <summary>AudioSource型の変数</summary>
    private AudioSource _audio = default;

    private void Start()
    {
        _blizzardPanel.enabled = false;
        _blizzardPanel.DOFade(0f, 0f);  //念のためここでGameObjectを透明にしておく
        _audio.DOFade(0f, 0f);
        if (_blizzardPanel is null)
            Debug.LogWarning("吹雪を起こしたいのですが、GameObjectがセットされていません。");
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float random = Random.Range(1f, 3f);

        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            _blizzardPanel.enabled = true;
            _blizzardPanel.DOFade(_alpha, random);
            _audio.Play();
            _audio.DOFade(1f, random);
            DOVirtual.DelayedCall(_stormTime, () =>
            {
                _blizzardPanel.DOFade(0f, random);
                _audio.DOFade(0f, random).OnComplete(() => _audio.Stop());
            }, false);
        }
    }
}
