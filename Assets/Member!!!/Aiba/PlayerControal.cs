using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControal : MonoBehaviour
{
    [SerializeField] string _avalacheTagName = "Avalanche";
    [SerializeField] string _goalTagName = "Goal";


    GameManager _gm;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _avalacheTagName)
        {
            _gm.GameOver();
        }

        if (collision.gameObject.tag == _goalTagName)
        {
            _gm.GameClear();
        }
    }


}
