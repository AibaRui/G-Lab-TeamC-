using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PauseManager;

public class snowBall : GimickBase
{
    GameObject p1;
    GameObject p2;
    [SerializeField] private string Player1;
    [SerializeField] private string Player2;
    [SerializeField] private float firePower = 0.05f;
    [SerializeField]private float _erased = 5;  //消えるまでの時間
    private float _exsited = 0;         //消えるまでのカウント
    bool isPause;
    
    // Start is called before the first frame update
   
    void Start()
    {
        p1 = GameObject.Find(Player1);
        p2 = GameObject.Find(Player2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == p1 || collision.gameObject == p2)
        {
            //スタン状態にする
            Destroy(this.gameObject);
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPause == false)
        {
            _exsited += Time.deltaTime;
            transform.Translate(new Vector2(-firePower, 0));
            if (_exsited > _erased)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override void GameOverPause()
    {
        isPause = true;
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
