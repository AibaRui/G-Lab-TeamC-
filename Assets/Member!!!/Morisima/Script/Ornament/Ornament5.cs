using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament5 : MonoBehaviour
{
    // Ornament ƒvƒŒƒ[ƒ“ƒg‚Ì‰ÁZ—p•Ï”
    public static int Ornament5Score = 0;

    void Start()
    {
        Ornament5Score = 0;
    }

    // Player ‚ª Ornament ‚É“–‚½‚Á‚½Á‚¦‚é
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament5Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
