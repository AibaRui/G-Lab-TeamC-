using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament6 : MonoBehaviour
{
    // Ornament ボールの加算用変数
    public static int Ornament6Score = 0;

    void Start()
    {
        Ornament6Score = 0;
    }

    // Player が Ornament に当たった時消える
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament6Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
