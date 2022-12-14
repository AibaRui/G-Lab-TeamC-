using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament1 : MonoBehaviour
{
    // Ornament ¯‚Ì‰ÁZ—p•Ï”
    public static int Ornament1Score = 0;
    

    void Start()
    {
        Ornament1Score = 0;
    }

    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament1Score += 1;
        Destroy(this.gameObject);
    }
}
