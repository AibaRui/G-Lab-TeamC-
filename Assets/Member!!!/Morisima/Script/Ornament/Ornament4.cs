using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament4 : MonoBehaviour
{
    // Ornament ‚­‚Â‚µ‚½‚Ì‰ÁZ—p•Ï”
    public static int Ornament4Score = 0;

    void Start()
    {
        Ornament4Score = 0;
    }

    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament4Score += 1;
        Destroy(this.gameObject);
    }
}
