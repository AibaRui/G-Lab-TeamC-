using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament1 : MonoBehaviour
{
    // Ornament ���̉��Z�p�ϐ�
    public static int Ornament1Score = 0;

    void Start()
    {
        Ornament1Score = 0;
    }

    // Player �� Ornament �ɓ���������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament1Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
