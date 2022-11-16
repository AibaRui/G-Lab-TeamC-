using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalGimmickBlocks : MonoBehaviour
{
    [Header("オーラによって変化するまでの時間")]
    [SerializeField] float _timeLimit = 5f;
    [SerializeField] float _timeCount = 0;

    [SerializeField] Color _colorHot;
    [SerializeField] Color _colorCool;

    [Header("氷の状態の時のレイヤー(Playerレイヤーと当たる)")]
    [Tooltip("氷の状態の時のレイヤー(Playerレイヤーと当たる)")] [SerializeField] int _coolLayer;

    [Header("水の状態の時のレイヤー(Playerレイヤーと当たらない)")]
    [Tooltip("水の状態の時のレイヤー(Playerレイヤーと当たらない)")] [SerializeField] int _hotLayer;

    [Header("熱の能力のタグ")]
    [SerializeField] string _hotTagName;

    [Header("冷気の能力のタグ")]
    [SerializeField] string _coolTagName;


    //現在、熱のオーラにあたっているかどうかを
    bool hot;
    //現在、冷気のオーラにあたっているかどうかを
    bool cool;

    [SerializeField] BrockState _brockState = BrockState.Ice;

    void Start()
    {
        ChangeBlock();
    }

    void Update()
    {        //冷気のオーラだけが当たっているとき
        if (!hot && cool)
        {
            //状態が熱だったら
            if (_brockState == BrockState.Hot)
            {
                _timeCount += Time.deltaTime;

                //一定時間当たっていたら状態を変化させる
                if (_timeCount >= _timeLimit)
                {
                    _brockState = BrockState.Ice;
                    _timeCount = 0;
                    ChangeBlock();
                }
            }
        }
        //熱のオーラだけが当たっているとき
        if (hot && !cool)
        {
            //状態が氷だったら
            if (_brockState == BrockState.Ice)
            {
                _timeCount += Time.deltaTime;

                //一定時間当たっていたら状態を変化させる
                if (_timeCount >= _timeLimit)
                {
                    _brockState = BrockState.Hot;
                    _timeCount = 0;
                    ChangeBlock();
                }
            }
        }

        //どっちも当たっていなかったら
        if (!hot && !cool)
        {
            //Debug.Log("no");
            //時間が以上だったら、元に戻す
            if (_timeCount > 0)
            {
                _timeCount -= Time.deltaTime;
                if (_timeCount < 0)
                {
                    _timeCount = 0;
                }
            }
        }
    }

    //Stateの状態に応じて、形状を変える
    void ChangeBlock()
    {
        if (_brockState == BrockState.Hot)
        {
            this.GetComponent<SpriteRenderer>().color = _colorHot;
            gameObject.layer = _hotLayer;
            
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = _colorCool;
            gameObject.layer = _coolLayer;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _coolTagName)
        {
            cool = true;
        }

        if (collision.gameObject.tag == _hotTagName)
        {
            hot = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _coolTagName)
        {
            cool = false;
        }

        if (collision.gameObject.tag == _hotTagName)
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
