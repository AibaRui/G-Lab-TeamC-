using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament5 : MonoBehaviour
{
    // Ornament プレゼントの加算用変数
    public static int Ornament5Score = 0;

    void Start()
    {
        Ornament5Score = 0;
    }

    // Player が Ornament に当たった時消える
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament5Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
