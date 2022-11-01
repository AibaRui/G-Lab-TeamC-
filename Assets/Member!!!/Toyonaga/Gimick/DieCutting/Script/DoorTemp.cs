using UnityEngine;
public class DoorTemp : MonoBehaviour
{
    [SerializeField, Tooltip("DieCutting‚ð“o˜^")]
    DieCutting dc_;
    private Vector3 moved;
    bool is_move_ = true;

    void Update()
    {
        if (dc_.Is_complete)
        {
            if(!is_move_) { return; }
            move();
        }
    }

    void move()
    {
        transform.position += new Vector3(0, 0.1f, 0);
        moved += new Vector3(0, 0.1f, 0);
        if(moved.magnitude > 5) { is_move_ = false; }
    }
}
