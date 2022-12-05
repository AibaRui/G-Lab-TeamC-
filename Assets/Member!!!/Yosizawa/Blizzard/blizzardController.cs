using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class blizzardController : MonoBehaviour
{
    [SerializeField, Tooltip("blizzard���N�������߂Ɏg��panel")]
    private Image _blizzardPanel = default;
    [SerializeField, Range(1f, 10f), Tooltip("�ǂ̂��炢�̎��Ԑ�����N������")]
    private float _stormTime;
    [SerializeField, Range(0f, 1f), Tooltip("blizzard���N�����Ƃ��Ɏg��panel��alpha�l")]
    private float _alpha;
    /// <summary>AudioSource�^�̕ϐ�</summary>
    private AudioSource _audio = default;

    private void Start()
    {
        _blizzardPanel.enabled = false;
        _blizzardPanel.DOFade(0f, 0f);  //�O�̂��߂�����GameObject�𓧖��ɂ��Ă���
        _audio.DOFade(0f, 0f);
        if (_blizzardPanel is null)
            Debug.LogWarning("������N���������̂ł����AGameObject���Z�b�g����Ă��܂���B");
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
