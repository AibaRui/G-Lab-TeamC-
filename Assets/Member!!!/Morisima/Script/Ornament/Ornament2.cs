using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament2 : MonoBehaviour
{
    // Ornament ���ڂ�����̉��Z�p�ϐ�
    public static int Ornament2Score = 0;

    void Start()
    {
        Ornament2Score = 0;
    }

    // Player �� Ornament �ɓ���������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament2Score += 1;
        Clear.Itemflag = true;
        Destroy(this.gameObject);
    }
}
