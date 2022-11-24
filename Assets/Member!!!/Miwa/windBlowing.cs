using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windBlowing : GimickBase
{
    [Header("風が吹く強さ(＝プレイヤーの速さ、orプレイヤーより少し弱い)"),SerializeField] private float wPower = 1f;         //風の力  
    [Header("プレイヤー1の設定"), SerializeField] private string player1 = null;
    [Header("プレイヤー2の設定"), SerializeField] private string player2 = null;
    [Header("風が吹く時間"),SerializeField] private float blowTime = 0;  //風が吹く時間
    [Header("プレイヤーが壁を判定する長さ"),SerializeField] private float playerRayLength = 5;
    float blowingTimer;
    bool isBlow;
    bool p1Hide;
    bool p2Hide;
    bool isPause;
    GameObject p1;
    GameObject p2;
    Rigidbody2D p1rb;
    Rigidbody2D p2rb;
    Vector2 p1Pos;
    Vector2 p2Pos;
    Vector2 playerDir;
    Ray2D p1Ray;
    Ray2D p2Ray;
    void Start()
    {
        p1 = GameObject.Find(player1);
        p2 = GameObject.Find(player2);
        p1rb = p1.GetComponent<Rigidbody2D>();
        p2rb = p2.GetComponent<Rigidbody2D>();
        playerDir = new Vector2(playerRayLength, 0);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        p1Pos = p1.transform.position ;
        p2Pos = p2.transform.position;
        p1Ray = new Ray2D(p1Pos, playerDir);
        p2Ray = new Ray2D(p2Pos, playerDir);
        RaycastHit2D p1hit = Physics2D.Raycast(p1Pos,Vector2.right, playerRayLength);
        RaycastHit2D p2hit = Physics2D.Raycast(p2Pos,Vector2.right, playerRayLength);
        if(p1hit.collider != null && p1hit.collider.gameObject.tag != "player")
        {
            p1Hide = true;
        }
        else { p1Hide = false; }
        if (p2hit.collider != null && p2hit.collider.gameObject.tag != "player")
        {
            p2Hide = true;
        }
        else { p2Hide= false; }


        if (isPause == false) { 
            if (isBlow)
            {
                blowingTimer += Time.deltaTime;
                if (blowingTimer < blowTime)
                {
                    if (p1Hide == false) { p1rb.AddForce(new Vector2(-wPower, 0)); }
                    if (p2Hide == false) { p2rb.AddForce(new Vector2(-wPower, 0)); }
                }
                else
                {
                    isBlow = false;
                }


            }
        }
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Space))        
        {
            blowing();
        }
        
        Debug.DrawRay(p1Pos, playerDir *1, Color.red);
        Debug.DrawRay(p2Pos, playerDir *1, Color.red);
        Debug.Log($"p1,{p1Hide}");
        Debug.Log(p2Hide);
    }

    public void blowing()   //風が吹くタイマーセット
    {
        blowingTimer = 0;
        isBlow = true;   
        
    }

    public override void GameOverPause() { 
        isPause= true;
    }

    public override void Pause()
    {
        isPause = true;
    }

    public override void Resume()
    {
        isPause = false;
    }
}
