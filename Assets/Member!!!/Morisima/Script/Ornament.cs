using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ornament : MonoBehaviour
{
    // Ornament ���̉��Z�p�ϐ�
    public static int Ornament1Score = 0;

    // Ornament ���ڂ�����̉��Z�p�ϐ�
    public static int Ornament2Score = 0;

    // Ornament �A�̉��Z�p�ϐ�
    public static int Ornament3Score = 0;

    // �c���[�p�� Ornament ���Z�p�ϐ�
    public static int Ornament4Score = 0;

    void Start()
    {
        Ornament1Score = 0;
        Ornament2Score = 0;
        Ornament3Score = 0;
        Ornament4Score = 0;
    }

    void Update()
    {
        
    }


    // Player �� Ornament �ɓ���������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
