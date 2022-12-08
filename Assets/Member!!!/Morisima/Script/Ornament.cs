using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ornament : MonoBehaviour
{    
    // Ornament ���̉��Z�p�ϐ�
    public static int Ornament1Score = 0;

    // Ornament ���ڂ�����̉��Z�p�ϐ�
    public static int Ornament2Score = 0;

    // Ornament �A�̉��Z�p�ϐ�
    public static int Ornament3Score = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    // Player �� Ornament �ɓ���������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ornament ���Z���č폜
        if(collision.gameObject.name == "Ornament1") { Ornament1Score += 1; }
        if(collision.gameObject.name == "Ornament2") { Ornament2Score += 1; }
        if(collision.gameObject.name == "Ornament3") { Ornament3Score += 1; }
        Destroy(this.gameObject);
    }

    // Ornament ���̎擾�֐�
    public static int GetOrnament1()
    {
        return Ornament1Score;
    }

    // Ornament ���ڂ�����̎擾�֐�
    public static int GetOrnament2()
    {
        return Ornament2Score;
    }

    // Ornament �A�̎擾�֐�
    public static int GetOrnament3()
    {
        return Ornament3Score;
    }
}
