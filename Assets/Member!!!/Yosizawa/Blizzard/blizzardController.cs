using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class blizzardController : MonoBehaviour
{
    [Header("blizzard���N�������߂Ɏg��panel")]
    [SerializeField,Tooltip("blizzard���N�������߂Ɏg��panel")] Image _blizzardPanel = default;
    [Header("�ǂ̂��炢�̎��Ԑ�����N������")]
    [SerializeField,Range(1f, 10f),Tooltip("���Ⴉ���鎞��")] float _stormTime;
    [Header("blizzard�̓����x")]
    [SerializeField, Range(0f, 1f), Tooltip("panel��alpha�l")] float _alpha;
    AudioSource _audio = default;

    void Start()
    {
        _blizzardPanel.enabled = false;
        _blizzardPanel.DOFade(0f, 0f);  //�O�̂��߂�����GameObject�𓧖��ɂ��Ă���
        _audio.DOFade(0f, 0f);
        if (_blizzardPanel is null)
            Debug.LogWarning("������N���������̂ł����AGameObject���Z�b�g����Ă��܂���B");
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
