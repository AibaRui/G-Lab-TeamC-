using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament4 : MonoBehaviour
{
    // Ornament くつしたの加算用変数
    public static int Ornament4Score = 0;

    void Start()
    {
        Ornament4Score = 0;
    }

    // Player が Ornament に当たった時消える
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament4Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
