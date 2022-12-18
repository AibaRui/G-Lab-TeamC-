using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament3 : MonoBehaviour
{
    // Ornament •A‚Ì‰ÁZ—p•Ï”
    public static int Ornament3Score = 0;

    void Start()
    {
        Ornament3Score = 0;
    }

    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament3Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
