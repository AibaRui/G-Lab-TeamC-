using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament6 : MonoBehaviour
{
    // Ornament ƒ{[ƒ‹‚Ì‰ÁZ—p•Ï”
    public static int Ornament6Score = 0;

    void Start()
    {
        Ornament6Score = 0;
    }

    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament6Score += 1;
        Destroy(this.gameObject);
    }
}
