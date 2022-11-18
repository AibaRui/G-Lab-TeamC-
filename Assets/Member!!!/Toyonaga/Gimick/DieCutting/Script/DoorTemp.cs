using UnityEngine;
public class DoorTemp : MonoBehaviour
{
    // ---- DieCuttingクラスの使用方法 ---- //
    [SerializeField, Tooltip("DieCuttingManagerを登録")]
    DieCuttingManager dc_;

    // - (本ギミックの変数) -
    private Vector3 moved;
    bool is_move_ = true;

    // ---- DieCuttingの使用例 ---- //
    void Update()
    {
        // --- ギミックが解けているとき：下記関数でTrueが返る --- //
        if (dc_.is_complete_)
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
