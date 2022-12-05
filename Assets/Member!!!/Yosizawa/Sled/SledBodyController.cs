using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class SledBodyController : MonoBehaviour
{
    /// <summary></summary>
    private SledController _sled;
    /// <summary>Rigidbody2D�^�̕ϐ�</summary>
    private Rigidbody2D _rb;
    /// <summary>�����蔻��𐧌�����</summary>
    private ContactFilter2D _filter;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //�ڐG������I�u�W�F�N�g�̑O�������ɐ�������
        _filter.useNormalAngle = true;
        _filter.minNormalAngle = 177f;
        _filter.maxNormalAngle = 183f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���ꂪ�A�^�b�`����Ă���I�u�W�F�N�g�ɏ������APlayer���ꏏ�ɓ����悤�ɂ���
        if (collision.gameObject.tag is "Player1" or "Player2")
        {
            collision.gameObject.transform.SetParent(transform);
        }

        //�O�������ڐG������s��
        if (_rb.IsTouching(_filter))
        {
            //�����A�I�u�W�F�N�g�̑��x�����ȏゾ������A�Փ˂����I�u�W�F�N�g��j�󂷂�
            if (_sled.Velocity.sqrMagnitude < _sled.MaxVelocity * 3 / 4)
            {
                _rb.AddForce(-_rb.velocity * 10, ForceMode2D.Impulse);
            }
            else
            {

            }
        }
    }
}
