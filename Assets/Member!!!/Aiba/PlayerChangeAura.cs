using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeAura : MonoBehaviour
{
    [Header("�v���C���[�̔ԍ�")]
    [Tooltip("�v���C���[�̔ԍ�")] [SerializeField] int _playerNumber = 1;

    [Header("�\�͂̌`")]
    [Tooltip("�\�͂̌`")] [SerializeField] GameObject[] _eria = new GameObject[1];



    [Header("�\�͂̌`�A��")]
    [Tooltip("�\�͂̌`�A��")] [SerializeField] GameObject _eriaUp;

    [Header("�\�͂̌`�A��")]
    [Tooltip("�\�͂̌`�A��")] [SerializeField] GameObject _eriaDown;

    [Header("�\�͂̌`�A�E")]
    [Tooltip("�\�͂̌`�A�E")] [SerializeField] GameObject _eriaRight;

    [Header("�\�͂̌`�A��")]
    [Tooltip("�\�͂̌`�A��")] [SerializeField] GameObject _eriaLeft;
    int count = 0;

    [SerializeField] Sousa _sousa = Sousa.Joistick;


    // ���S�_
    [SerializeField] private Vector3 _center;

    // �~�^������
    [SerializeField] private float _period = 2;




    void Start()
    {

        _eria[count].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_sousa == Sousa.Bbuttun)
        {
            ChangeAuraController();
        }
        else
        {
            ChangeAuraKeyBord();
        }


    }

    void ChangeAuraController()
    {
        if (Input.GetButtonDown($"AuraChange{_playerNumber}"))
        {
            _eria[count].SetActive(false);
            count++;
            if (count == _eria.Length)
            {
                count = 0;
            }
            _eria[count].SetActive(true);
        }
    }


    void ChangeAuraKeyBord()
    {
        //float h = Input.GetAxisRaw($"AuraChangeJoistickHorizontal{_playerNumber}");
        //float v = Input.GetAxisRaw($"AuraChangeJoistickVertical{_playerNumber}");

        //if (h != 0 || v != 0)
        //{
        //    _center = transform.position;

        //    if (h > 0)
        //    {
        //        if(_eriaUp.transform.rotation.x<=90)
        //        {
        //        _period = Mathf.Abs(_period);
        //        }
        //        else
        //        {
        //            _period
        //        }

        //    }
        //    else if (h < 0)
        //    {

        //    }
        //    else if (v > 0)
        //    {

        //    }
        //    else if (v < 0)
        //    {

        //    }



        var tr = _eriaUp.transform;
        // ��]�̃N�H�[�^�j�I���쐬
        var angleAxis = Quaternion.AngleAxis(360 / _period * Time.deltaTime, transform.forward);

        // �~�^���̈ʒu�v�Z
        var pos = tr.position;

        _center = transform.position;
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //-90
            _period = -2;


            var a = tr.transform.rotation.z - 90;
            while (tr.rotation.z > a)
            {
                pos -= _center;
                pos = angleAxis * pos;
                pos += _center;

                tr.position = pos;
                tr.rotation = tr.rotation * angleAxis;
            }



        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            var a = tr.transform.rotation.z + 90;
            _period = 2;
            while (tr.rotation.x < a)
            {
                pos -= _center;
                pos = angleAxis * pos;
                pos += _center;

                tr.position = pos;
                tr.rotation = tr.rotation * angleAxis;
            }

        }






    }






    enum Sousa
    {
        Joistick,
        Bbuttun,
    }

}
