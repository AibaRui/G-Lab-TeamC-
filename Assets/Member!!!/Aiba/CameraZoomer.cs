using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomer : MonoBehaviour
{

    [Header("TargetGroup���Ă������")]
    [Tooltip("TargetGroup���Ă������")] [SerializeField] CinemachineTargetGroup _cinemachineTargetGroup;

    [Header("�l���グ��قǋ����Y�[�������")]
    [Tooltip("TargetGroup���Ă������")] [SerializeField] float _num = 3;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            _cinemachineTargetGroup.m_Targets[0].weight = _num;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            _cinemachineTargetGroup.m_Targets[1].weight = _num;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            _cinemachineTargetGroup.m_Targets[0].weight = 1;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            _cinemachineTargetGroup.m_Targets[1].weight = 1;
        }
    }

}
