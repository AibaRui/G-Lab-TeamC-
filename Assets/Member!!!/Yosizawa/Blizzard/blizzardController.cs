using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class blizzardController : MonoBehaviour
{
    [Header("blizzardを起こすために使うpanel")]
    [SerializeField,Tooltip("blizzardを起こすために使うpanel")] Image _blizzardPanel = default;
    [Header("どのくらいの時間吹雪を起こすか")]
    [SerializeField,Range(1f, 10f),Tooltip("吹雪かせる時間")] float _stormTime;
    [Header("blizzardの透明度")]
    [SerializeField, Range(0f, 1f), Tooltip("panelのalpha値")] float _alpha;
    AudioSource _audio = default;

    void Start()
    {
        _blizzardPanel.enabled = false;
        _blizzardPanel.DOFade(0f, 0f);  //念のためここでGameObjectを透明にしておく
        _audio.DOFade(0f, 0f);
        if (_blizzardPanel == null)
            Debug.LogWarning("吹雪を起こしたいのですが、GameObjectがセットされていません。");
        _audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
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
