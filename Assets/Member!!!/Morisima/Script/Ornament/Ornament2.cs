using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament2 : MonoBehaviour
{
    // Ornament 松ぼっくりの加算用変数
    public static int Ornament2Score = 0;

    void Start()
    {
        Ornament2Score = 0;
    }

    // Player が Ornament に当たった時消える
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament2Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
