using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ornament : MonoBehaviour
{    
    // Ornament 星の加算用変数
    public static int Ornament1Score = 0;

    // Ornament 松ぼっくりの加算用変数
    public static int Ornament2Score = 0;

    // Ornament 柊の加算用変数
    public static int Ornament3Score = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    // Player が Ornament に当たった時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ornament 加算して削除
        if(collision.gameObject.name == "Ornament1") { Ornament1Score += 1; }
        if(collision.gameObject.name == "Ornament2") { Ornament2Score += 1; }
        if(collision.gameObject.name == "Ornament3") { Ornament3Score += 1; }
        Destroy(this.gameObject);
    }

    // Ornament 星の取得関数
    public static int GetOrnament1()
    {
        return Ornament1Score;
    }

    // Ornament 松ぼっくりの取得関数
    public static int GetOrnament2()
    {
        return Ornament2Score;
    }

    // Ornament 柊の取得関数
    public static int GetOrnament3()
    {
        return Ornament3Score;
    }
}
