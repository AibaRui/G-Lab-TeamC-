using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blocks : MonoBehaviour
{
    [SerializeField] float _timeLimit = 5f;
    [SerializeField] float _timeCount = 0;

    [SerializeField] Color _colorHot;
    [SerializeField] Color _colorCool;

    bool hot;
    bool cool;


  [SerializeField]  BrockState _brockState = BrockState.Ice;

    void Start()
    {

    }

    void Update()
    {
        a();

        if (_brockState == BrockState.Ice)
        {
            if (hot && !cool)
            {
                _timeCount += Time.deltaTime;
                if (_timeCount >= _timeLimit)
                {
                    _brockState = BrockState.Hot;
                    _timeCount =0;
                }
            }
            else
            {
                if (_timeCount >= 0)
                {
                    _timeCount -= Time.deltaTime;
                }

            }
        }

        if (_brockState == BrockState.Hot)
        {
            if (!hot && cool)
            {
                _timeCount += Time.deltaTime;
                if (_timeCount >= _timeLimit)
                {
                    _brockState = BrockState.Ice;
                    _timeCount = 0;
                }
            }
            else
            {
                if (_timeCount >= 0)
                {
                    _timeCount -= Time.deltaTime;
                }

            }
        }
    }

    void a()
    {
        if (_brockState == BrockState.Hot)
        {
            this.GetComponent<SpriteRenderer>().color = _colorHot;
            gameObject.layer = 6;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = _colorCool;
            gameObject.layer = 7;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1")
        {
            cool = true;

        }

        if (collision.gameObject.tag == "P2")
        {
            hot = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1")
        {
            cool = false;

        }

        if (collision.gameObject.tag == "P2")
        {
            hot = false;
        }
    }


    enum BrockState
    {
        Ice,
        Hot,
    }


}
