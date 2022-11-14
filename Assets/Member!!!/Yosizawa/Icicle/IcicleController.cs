using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IcicleController : MonoBehaviour
{
    [Header("�������x")]
    [SerializeField] float _fallSpeed = 1.0f;
    [Header("�I�[���ڐG���̊g��E�k���{��")]
    [SerializeField,Range(1f, 10f),Tooltip("")] float _magnification = 1.0f;
    [Header("GameObject�������ɍĐ�����Animetion")]
    [SerializeField] GameObject _onDestroyAnimation = default;
    [Header("Player�����m��������� �\�� or ��\��")]
    [SerializeField,Tooltip("Ray�̕\���E��\���̐؂�ւ�")] bool _isGizmo = false;
    [Header("Player�����m����������΂�����")]
    [SerializeField,Range(-5f, 5f),Tooltip("Ray���΂�����")] float _direction = 1f;
    [Header("Player�����m����������΂�����")]
    [SerializeField,Range(0f, 15f),Tooltip("Ray�̋���")] float _length = 1f;
    /// <summary>Ray���΂�����</summary>
    Vector2 _dir = Vector2.zero;
    /// <summary>Ray���΂��ē�������collider�̏��</summary>
    RaycastHit2D _hit;
    Rigidbody2D _rb;

    void Start()
    {
        _magnification /= 100f;  //�������Ɣ{������������̂ŁA���炩���ߒႭ���Ă���
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    void Update()
    {
        //Ray�ɓ�������collider��Player���ǂ������肷��
        _hit = Physics2D.Raycast(transform.position, _dir, _length, LayerMask.GetMask("Player"));

        if(_hit.collider)  //Ray��Player�ɓ����������̏���
        {
            _rb.gravityScale = _fallSpeed;
            Debug.Log("Check In");
        }
    }

    /// <summary>Ray���������邽�߂̊֐�</summary>
    void OnDrawGizmos()
    {
        if (_isGizmo == false) return;

        _dir = new Vector2(_direction, -1).normalized;
        Gizmos.DrawRay(transform.position, _dir * _length);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //�Q�[���I�u�W�F�N�g�̑傫���̏�񂪓������ϐ�
        var localScale = transform.localScale;

        //�Q�[���I�u�W�F�N�g���ɒ[�ɑ傫��(������)�Ȃ�Ȃ��悤���Ă���
        localScale.x = Mathf.Clamp(localScale.x, 3f, 6f);
        localScale.y = Mathf.Clamp(localScale.y, 3f, 6f);

        //���������I�[�����u�n�����v�Ȃ�Q�[���I�u�W�F�N�g�̑傫��������
        if (collision.gameObject.tag is "Hot")
        {
            localScale.x -= _magnification;
            localScale.y -= _magnification;
            transform.localScale = localScale;
        }
        //���������I�[�����u�ł߂�v�Ȃ�Q�[���I�u�W�F�N�g�̑傫���𑝉�
        if (collision.gameObject.tag is "Cool")
        {
            localScale.x += _magnification;
            localScale.y += _magnification;
            transform.localScale = localScale;
        }
    }
}