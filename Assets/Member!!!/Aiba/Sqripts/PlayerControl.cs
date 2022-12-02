using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] string _goalTagName = "";
    [SerializeField] string _nadareTagName = "";

    GameManager _gm;


    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == _goalTagName)
        {
            _gm.GameClear();
        }

        if (collision.gameObject.tag == _nadareTagName)
        {
            _gm.GameOver();
        }

    }

}
