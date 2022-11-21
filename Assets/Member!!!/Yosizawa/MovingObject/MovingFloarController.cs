using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MovingFloarController : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag is "Player1" or "Player2")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
}
