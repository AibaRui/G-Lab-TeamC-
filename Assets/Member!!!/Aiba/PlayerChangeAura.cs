using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeAura : MonoBehaviour
{
    [Header("プレイヤーの番号")]
    [Tooltip("プレイヤーの番号")] [SerializeField] int _playerNumber = 1;

    [Header("能力の形")]
    [Tooltip("能力の形")] [SerializeField] GameObject[] _eria = new GameObject[1];



    [Header("能力の形、上")]
    [Tooltip("能力の形、上")] [SerializeField] GameObject _eriaUp;

    [Header("能力の形、下")]
    [Tooltip("能力の形、下")] [SerializeField] GameObject _eriaDown;

    [Header("能力の形、右")]
    [Tooltip("能力の形、右")] [SerializeField] GameObject _eriaRight;

    [Header("能力の形、左")]
    [Tooltip("能力の形、左")] [SerializeField] GameObject _eriaLeft;
    int count = 0;

    [SerializeField] Sousa _sousa = Sousa.Joistick;


    // 中心点
    [SerializeField] private Vector3 _center;

    // 円運動周期
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
        // 回転のクォータニオン作成
        var angleAxis = Quaternion.AngleAxis(360 / _period * Time.deltaTime, transform.forward);

        // 円運動の位置計算
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
