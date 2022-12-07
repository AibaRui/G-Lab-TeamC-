using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]

public class blizzardController : MonoBehaviour
{
    [SerializeField, Tooltip("吹雪を起こすために使うpanel")]
    private Image _blizzardPanel = null;
    /// <summary>吹雪を起こすために使うpanelのprefab</summary>
    private Image _panelPrefab;
    [SerializeField, Tooltip("吹雪のパーティクル")] 
    private ParticleSystem _blizzard = null;
    /// <summary>吹雪のパーティクルのprefab</summary>
    private ParticleSystem _particlePrefab;
    [SerializeField, Range(1f, 10f), Tooltip("どのくらいの時間吹雪を起こすか")]
    private float _stormTime;
    [SerializeField, Range(0f, 1f), Tooltip("blizzardを起こすときに使うpanelのalpha値")]
    private float _alpha;
    /// <summary>AudioSource型の変数</summary>
    private AudioSource _audio = null;

    private void Start()
    {
        GameObject particle;
        //吹雪のパーティクルをカメラの子オブジェクトにする
        GameObject camera = GameObject.Find("Main Camera");
        particle = Instantiate(_blizzard.gameObject, camera.transform);
        _particlePrefab = particle.GetComponent<ParticleSystem>();

        GameObject panel;
        //吹雪のパネルをキャンバスの子オブジェクトにする
        GameObject canvas = GameObject.Find("Canvas");
        panel = Instantiate(_blizzardPanel.gameObject, canvas.transform);
        _panelPrefab = panel.GetComponent<Image>();
        _blizzardPanel.DOFade(0f, 0f);  //ここでGameObjectを透明にしておく
        if (_blizzardPanel is null)
            Debug.LogWarning("吹雪を起こしたいのですが、GameObjectがセットされていません。");

        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float random = Random.Range(1f, 3f);

        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            _panelPrefab.DOFade(_alpha, random);
            _particlePrefab.Play();
            _audio.Play();
            _audio.DOFade(1f, random);
            Debug.Log("Clear!");
            DOVirtual.DelayedCall(_stormTime, () =>
            {
                Debug.Log("Great!");
                _panelPrefab.DOFade(0f, random);
                _audio.DOFade(0f, random).OnComplete(() =>
                {
                    _audio.Stop();
                    _particlePrefab.Stop();
                    Debug.Log("Perfect!");
                });
            }, false);
        }
    }
}
