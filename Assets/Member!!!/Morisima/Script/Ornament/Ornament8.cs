using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament8 : MonoBehaviour
{
    // Ornament 雪の結晶の加算用変数
    public static int Ornament8Score = 0;

    void Start()
    {
        Ornament8Score = 0;
    }

    // Player が Ornament に当たった時消える
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament8Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
