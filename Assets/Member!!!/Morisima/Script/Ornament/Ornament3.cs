using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament3 : MonoBehaviour
{
    // Ornament �A�̉��Z�p�ϐ�
    public static int Ornament3Score = 0;

    void Start()
    {
        Ornament3Score = 0;
    }

    // Player �� Ornament �ɓ���������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament3Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
