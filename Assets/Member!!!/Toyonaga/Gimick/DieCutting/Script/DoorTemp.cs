using UnityEngine;
public class DoorTemp : MonoBehaviour
{
    [SerializeField, Tooltip("DieCuttingを登録")]
    DieCutting dc_;
    private Vector3 moved;
    bool is_move_ = true;

    // ---- DieCuttingの使用例 ---- //
    void Update()
    {
        // --- ギミックが解けているとき：下記関数でTrueが返る --- //
        if (dc_.IsComplete())
        {
            // -- あとはそれに応じて扉等のギミックを動かす -- //
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
