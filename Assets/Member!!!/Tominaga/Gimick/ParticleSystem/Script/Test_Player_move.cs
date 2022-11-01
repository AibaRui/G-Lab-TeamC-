using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_move : MonoBehaviour
{
    public Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rb2D.AddForce(new Vector2(h * 5, v * 5));

    }
}
