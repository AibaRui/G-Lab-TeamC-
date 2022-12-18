using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControlSystem : MonoBehaviour
{

    [Header("Player1")]
    [Tooltip("Player1")] [SerializeField] PlayerControl playerControl1;

    [Header("Player2")]
    [Tooltip("Player2")] [SerializeField] PlayerControl playerControl2;

    [SerializeField] Text _text;



   public void ChacgeKeyBord()
    {
        playerControl1.ChangeControlOnKeyBord();
        playerControl2.ChangeControlOnKeyBord();
        _text.text = "KeyBord";
    }


   public void ChacgeController()
    {
        playerControl1.ChangeControlOnController();
        playerControl2.ChangeControlOnController();
        _text.text = "Controller";
    }

}
