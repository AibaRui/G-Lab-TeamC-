using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageble : MonoBehaviour
{
    [SerializeField] GameObject _player1;
    [SerializeField] GameObject _player2;

    [SerializeField] Transform pos;

    public void Damege()
    {
        var r = Random.Range(0, 2);


        if(r==0)
        {
            _player1.GetComponent<IDamagable>().AddDamage(pos,2);
        }
        else
        {
            _player2.GetComponent<IDamagable>().AddDamage(pos, 2);
        }
    }

}
