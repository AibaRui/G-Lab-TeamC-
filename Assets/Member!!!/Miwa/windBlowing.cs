using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windBlowing : MonoBehaviour
{
    [SerializeField] private float wPower = 1f;         //風の力  
    [SerializeField] private string player1 = null;
    [SerializeField] private string player2 = null;
    [SerializeField] private float blowTime = 0;       //風が吹く時間
    float blowingTimer;
    bool isBlow;
    Rigidbody2D p1;
    Rigidbody2D p2;
    void Start()
    {
        p1 = GameObject.Find(player1).GetComponent<Rigidbody2D>();
        p2 = GameObject.Find(player2).GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isBlow)     
        {
            blowingTimer += Time.deltaTime;
            if (blowingTimer < blowTime)
            {
                p1.AddForce(new Vector2(-wPower, 0));
                p2.AddForce(new Vector2(-wPower, 0));
            }
            else
            {
                isBlow = false;
            }
        }
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Space))        
        {
            blowing();
        }
    }

    public void blowing()   //風が吹くタイマーセット
    {
        blowingTimer = 0;
        isBlow = true;   
        
    }
}
