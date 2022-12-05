using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledController : MonoBehaviour
{
    [SerializeField, Tooltip("�I�u�W�F�N�g�̉����x")]
    private float _acceleration = 1f;
    [SerializeField, Tooltip("�I�u�W�F�N�g�̍ő呬�x")]
    private float _maxVelocity = 1f;
    [SerializeField, Tooltip("���ۂɓ������I�u�W�F�N�g")]
    private GameObject _movingObj;
    /// <summary>assign����Ă���I�u�W�F�N�g��Rigidbody2D</summary>
    private Rigidbody2D _objRb;
    ///// <summary>�I�u�W�F�N�g�̑��x</summary>
    private Vector2 _velocity;

    /// <summary>�����o�[�ϐ��ł���_maxVelocity�̃v���p�e�B(�ǂݎ���p)</summary>
    public float MaxVelocity => _maxVelocity;

    /// <summary>�����o�[�ϐ��ł���_velocity�̃v���p�e�B</summary>
    public Vector2 Velocity
    {
        private set
        {
            value.x = Mathf.Clamp(_velocity.x, 0f, _maxVelocity);
            value.y = 0;
            _velocity = value;
        }
        get { return _velocity; }
    }

    private void Start()
    {
        //_movingObj�ƈꏏ�ɓ����Ăق����̂ŁA_movingObj�̎q�I�u�W�F�N�g�ɂ��Ă���
        transform.SetParent(_movingObj.transform);

        //SledBodyController���A�^�b�`����Ă��邱�Ƃ��m�񂷂�
        if (!_movingObj.GetComponent<SledBodyController>())
        {
            _movingObj.AddComponent<SledBodyController>();
        }
        _objRb = _movingObj.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //[�n����]�I�[��������������A��������
        if (collision.gameObject.tag is "Hot")
        {
            _velocity = new Vector2(_objRb.velocity.x * _acceleration, 0);
        }

        //[�ł߂�]�I�[��������������A��������
        if (collision.gameObject.tag is "Cool")
        {
            _velocity = new Vector2(_objRb.velocity.x * -_acceleration, 0);
        }
    }
}
