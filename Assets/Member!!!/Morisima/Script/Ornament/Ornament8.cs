using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament8 : MonoBehaviour
{
    // Ornament ��̌����̉��Z�p�ϐ�
    public static int Ornament8Score = 0;

    void Start()
    {
        Ornament8Score = 0;
    }

    // Player �� Ornament �ɓ���������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament8Score += 1;
        Destroy(this.gameObject);
    }
}
