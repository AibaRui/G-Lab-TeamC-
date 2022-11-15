using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windBlowing : MonoBehaviour
{
    [SerializeField] private float wPower = 1f;         //風の力  
    [SerializeField] private string player1 = null;
    [SerializeField] private string player2 = null;
    [SerializeField] private float blowTime = 0;  //風が吹く時間
    [SerializeField] private float playerRayLength = 5;
    float blowingTimer;
    bool isBlow;
    GameObject p1;
    GameObject p2;
    Rigidbody2D p1rb;
    Rigidbody2D p2rb;
    Vector2 p1Pos;
    Vector2 p2Pos;
    Vector2 p1Dir;
    Vector2 p2Dir;
    Ray2D p1Ray;
    Ray2D p2Ray;
    void Start()
    {
        p1 = GameObject.Find(player1);
        p2 = GameObject.Find(player2);
        p1rb = p1.GetComponent<Rigidbody2D>();
        p2rb = p2.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        p1Pos = p1.transform.position;
        p2Pos = p2.transform.position;
        p1Dir = new Vector2(p1.transform.position.x + playerRayLength, p1.transform.position.y);
        p2Dir = new Vector2(p2.transform.position.x + playerRayLength, p2.transform.position.y);
        p1Ray = new Ray2D(p1Pos, p1Dir);
        p2Ray = new Ray2D(p2Pos, p2Dir);
        if (isBlow)     
        {
            blowingTimer += Time.deltaTime;
            if (blowingTimer < blowTime)
            {
                p1rb.AddForce(new Vector2(-wPower, 0));
                p2rb.AddForce(new Vector2(-wPower, 0));
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
        Debug.DrawRay(p1Pos, p1Dir*1, Color.red);
        Debug.DrawRay(p2Pos, p2Dir*1, Color.red);
    }

    public void blowing()   //風が吹くタイマーセット
    {
        blowingTimer = 0;
        isBlow = true;   
        
    }
}
