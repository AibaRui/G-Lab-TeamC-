using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament : MonoBehaviour
{
    // Ornament 星の加算用変数
    public static int Ornament1Score = 0;

    // Ornament 松ぼっくりの加算用変数
    public static int Ornament2Score = 0;

    // Ornament 柊の加算用変数
    public static int Ornament3Score = 0;

    // ツリー用の Ornament 加算用変数
    public static int Ornament4Score = 0;

    void Start()
    {
        Ornament1Score = 0;
        Ornament2Score = 0;
        Ornament3Score = 0;
        Ornament4Score = 0;
    }

    void Update()
    {
        
    }


    // Player が Ornament に当たった時消える
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
