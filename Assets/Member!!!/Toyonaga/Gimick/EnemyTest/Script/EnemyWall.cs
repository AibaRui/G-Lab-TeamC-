using System.Collections.Generic;
using UnityEngine;

public class EnemyWall : MonoBehaviour
{
    [SerializeField, Header("壁と認識するタグ名を追記")]
    List<string> wall_tags_;
    private bool on_wall_ = false;
    public bool On_wall { get { return on_wall_; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool is_on_wall = false;
        for(var i = 0; i < wall_tags_.Count; i++)
        {
            if(collision.tag == wall_tags_[i])
            {
                is_on_wall = true;
                break;
            }
        }
        on_wall_ = is_on_wall;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        on_wall_ = false;
    }
}
