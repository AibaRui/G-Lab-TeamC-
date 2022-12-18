using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D),typeof(AudioSource))]

class TestShellController : GimickBase
{
    [SerializeField, Tooltip("�e����������܂ł̎���")]
    private float _lifeTime = 3f;
    /// <summary>�o�ߎ��Ԃ𑪒肷��^�C�}�[</summary>
    private float _timer = 0;
    [SerializeField, Tooltip("Player�ɒ��e�������ɉ������")]
    private float _forcePower = 1f;
    /// <summary>�e�ɃA�^�b�`����Ă���R���C�_�[</summary>
    private CircleCollider2D _col;
    /// <summary>���݁APause�����ǂ����𔻒肷��t���O</summary>
    private bool _isPause = false;
    /// <summary>�Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���Rigidbody2D</summary>
    private Rigidbody2D _rb;
    /// <summary>�C��̃Q�[���I�u�W�F�N�g</summary>
    GameObject _turret = null;
    /// <summary>��ʂ����e�������ɍĐ�����SE</summary>
    private AudioSource _audio = null;

    //�p�����[�^�[��ۑ�����p�̕ϐ�
    private float _saveAngularVelocity;
    private Vector2 _saveVelocity;

    private void Start()
    {
        _col = GetComponent<CircleCollider2D>();
        _col.isTrigger = true;
        _rb = GetComponent<Rigidbody2D>();
        _turret = FindObjectOfType<TurretController>().gameObject;
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!_isPause)  //
        {
            _timer = Time.deltaTime;
            if(_timer >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag is "Player1" or "Player2")
        {
            GameObject player = collision.gameObject;
            AudioSource.PlayClipAtPoint(_audio.clip, transform.position);

            if (player.transform.position.x <= _turret.transform.position.x)
            {
                player.GetComponent<IDamagable>().AddDamage(transform, _forcePower);
                Debug.Log("Go! Left!");
            }
            else
            {
                player.GetComponent<IDamagable>().AddDamage(transform, -_forcePower);
                Debug.Log("Go! Right!");
            }
            Destroy(gameObject);
            Debug.Log("HIT");
        }
    }

    public override void GameOverPause()
    {
        _isPause = true;
        _saveAngularVelocity = _rb.angularVelocity;
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Pause()
    {
        _isPause = true;
        _saveAngularVelocity = _rb.angularVelocity;
        _saveVelocity = _rb.velocity;
        _rb.Sleep();
        _rb.simulated = false;
    }

    public override void Resume()
    {
        _rb.simulated = true;
        _rb.WakeUp();
        _rb.angularVelocity = _saveAngularVelocity;
        _rb.velocity = _saveVelocity;
        _isPause = false;
    }
}
