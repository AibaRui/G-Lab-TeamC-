using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament7 : MonoBehaviour
{
    // Ornament ��̌����̉��Z�p�ϐ�
    public static int Ornament7Score = 0;

    void Start()
    {
        Ornament7Score = 0;
    }

    // Player �� Ornament �ɓ���������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ornament7Score += 1;
        Destroy(this.gameObject);
    }
}
