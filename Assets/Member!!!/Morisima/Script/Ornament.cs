using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament : MonoBehaviour
{
    // Ornament ¯‚Ì‰ÁZ—p•Ï”
    public static int Ornament1Score = 0;

    // Ornament ¼‚Ú‚Á‚­‚è‚Ì‰ÁZ—p•Ï”
    public static int Ornament2Score = 0;

    // Ornament •A‚Ì‰ÁZ—p•Ï”
    public static int Ornament3Score = 0;

    // ƒcƒŠ[—p‚Ì Ornament ‰ÁZ—p•Ï”
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


    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
