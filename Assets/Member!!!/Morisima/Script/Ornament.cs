using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ornament : MonoBehaviour
{    
    // Ornament ¯‚Ì‰ÁZ—p•Ï”
    public static int Ornament1Score = 0;

    // Ornament ¼‚Ú‚Á‚­‚è‚Ì‰ÁZ—p•Ï”
    public static int Ornament2Score = 0;

    // Ornament •A‚Ì‰ÁZ—p•Ï”
    public static int Ornament3Score = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    // Player ‚ª Ornament ‚É“–‚½‚Á‚½
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ornament ‰ÁZ‚µ‚Äíœ
        if(collision.gameObject.name == "Ornament1") { Ornament1Score += 1; }
        if(collision.gameObject.name == "Ornament2") { Ornament2Score += 1; }
        if(collision.gameObject.name == "Ornament3") { Ornament3Score += 1; }
        Destroy(this.gameObject);
    }

    // Ornament ¯‚Ìæ“¾ŠÖ”
    public static int GetOrnament1()
    {
        return Ornament1Score;
    }

    // Ornament ¼‚Ú‚Á‚­‚è‚Ìæ“¾ŠÖ”
    public static int GetOrnament2()
    {
        return Ornament2Score;
    }

    // Ornament •A‚Ìæ“¾ŠÖ”
    public static int GetOrnament3()
    {
        return Ornament3Score;
    }
}
