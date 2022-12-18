using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomer : MonoBehaviour
{

    [Header("TargetGroup‚Á‚Ä‚â‚Â‚ğ“ü‚ê‚é")]
    [Tooltip("TargetGroup‚Á‚Ä‚â‚Â‚ğ“ü‚ê‚é")] [SerializeField] CinemachineTargetGroup _cinemachineTargetGroup;

    [Header("’l‚ğã‚°‚é‚Ù‚Ç‹­‚­ƒY[ƒ€‚³‚ê‚é")]
    [Tooltip("TargetGroup‚Á‚Ä‚â‚Â‚ğ“ü‚ê‚é")] [SerializeField] float _num = 3;


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
