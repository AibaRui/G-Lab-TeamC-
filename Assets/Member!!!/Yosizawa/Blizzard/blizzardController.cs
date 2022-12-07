using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]

public class blizzardController : MonoBehaviour
{
    [SerializeField, Tooltip("������N�������߂Ɏg��panel")]
    private Image _blizzardPanel = null;
    /// <summary>������N�������߂Ɏg��panel��prefab</summary>
    private Image _panelPrefab;
    [SerializeField, Tooltip("����̃p�[�e�B�N��")] 
    private ParticleSystem _blizzard = null;
    /// <summary>����̃p�[�e�B�N����prefab</summary>
    private ParticleSystem _particlePrefab;
    [SerializeField, Range(1f, 10f), Tooltip("�ǂ̂��炢�̎��Ԑ�����N������")]
    private float _stormTime;
    [SerializeField, Range(0f, 1f), Tooltip("blizzard���N�����Ƃ��Ɏg��panel��alpha�l")]
    private float _alpha;
    /// <summary>AudioSource�^�̕ϐ�</summary>
    private AudioSource _audio = null;

    private void Start()
    {
        GameObject particle;
        //����̃p�[�e�B�N�����J�����̎q�I�u�W�F�N�g�ɂ���
        GameObject camera = GameObject.Find("Main Camera");
        particle = Instantiate(_blizzard.gameObject, camera.transform);
        _particlePrefab = particle.GetComponent<ParticleSystem>();

        GameObject panel;
        //����̃p�l�����L�����o�X�̎q�I�u�W�F�N�g�ɂ���
        GameObject canvas = GameObject.Find("Canvas");
        panel = Instantiate(_blizzardPanel.gameObject, canvas.transform);
        _panelPrefab = panel.GetComponent<Image>();
        _blizzardPanel.DOFade(0f, 0f);  //������GameObject�𓧖��ɂ��Ă���
        if (_blizzardPanel is null)
            Debug.LogWarning("������N���������̂ł����AGameObject���Z�b�g����Ă��܂���B");

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
