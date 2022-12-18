using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament7 : MonoBehaviour
{
    // Ornament á‚ÌŒ‹»‚Ì‰ÁZ—p•Ï”
    public static int Ornament7Score = 0;

    void Start()
    {
        Ornament7Score = 0;
    }

    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament7Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
