using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament2 : MonoBehaviour
{
    // Ornament ¼‚Ú‚Á‚­‚è‚Ì‰ÁZ—p•Ï”
    public static int Ornament2Score = 0;

    void Start()
    {
        Ornament2Score = 0;
    }

    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament2Score += 1;
        Destroy(this.gameObject);
    }
}
