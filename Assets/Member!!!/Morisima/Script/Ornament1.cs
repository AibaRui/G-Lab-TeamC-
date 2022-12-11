using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament1 : MonoBehaviour
{
    // Ornament 星の加算用変数
    public static int Ornament1Score = 0;

    void Start()
    {
        Ornament1Score = 0;
    }

    // Player が Ornament に当たった時消える
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament1Score += 1;
        Destroy(this.gameObject);
    }
}
