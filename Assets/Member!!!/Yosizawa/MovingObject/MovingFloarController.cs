using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloarController : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
}
