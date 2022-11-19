using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField, Header("MoveArea : 行動範囲")]
    BoxCollider2D move_lim_boxCol_;
    [SerializeField, Header("移動速度[1マス/s]")]
    private float move_velocity = 1f;
    [SerializeField, Header("RigidBody2D")]
    Rigidbody2D rb2d_;
    [SerializeField, Header("壁判定")]
    EnemyWall wall_check_;
    
    private Vector2[] move_limits_ = new Vector2[2];
    private int dir_ = -1;                  // 移動方向

    enum enemy_mode_
    {
        search, attack, box, melt,
    }
    private enemy_mode_ e_mode = enemy_mode_.search;

    void Start()
    {
        // ----- 初期化 ----- //
        // ---- 行動限界範囲設定(四角形)
        move_limits_[0] = new Vector2(
            move_lim_boxCol_.transform.position.x - move_lim_boxCol_.size.x/2,
            move_lim_boxCol_.transform.position.y + move_lim_boxCol_.size.y/2
            );
        move_limits_[1] = new Vector2(
            move_lim_boxCol_.transform.position.x + move_lim_boxCol_.size.x / 2,
            move_lim_boxCol_.transform.position.y - move_lim_boxCol_.size.y / 2
            );
        // MoveArea：以上操作より用無し。消す
        move_lim_boxCol_.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 0. 偵察モード 
        if (enemy_mode_.search == e_mode)
        {
            // ---- 移動＆壁があったら反転 ---- //
            GroundEnemyMove(Time.fixedDeltaTime, ref rb2d_, ref wall_check_);
        }



       
    }

    private void GroundEnemyMove(float delta_time, ref Rigidbody2D rb, ref EnemyWall w_check)
    {
        // ---- 地上敵の通常移動：移動範囲内で行動 ---- //
        // --- 移動 --- //
        Vector3 velocity = new Vector3(move_velocity * dir_ * Time.fixedDeltaTime, 0, 0);
        rb.transform.position += velocity;
        if ((rb.transform.position.x < move_limits_[0].x) || (rb.transform.position.x > move_limits_[1].x)) {
            dir_ *= -1;
            transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
        
        // --- 壁判定 --- //
        }else if (w_check.On_wall) {
            dir_ *= -1;
            transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
        }
    }



}
