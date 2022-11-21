using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("索敵対象のタグ名：SearchArea内にいれば発見＆攻撃する")]
    private List<string> target_tag_;
    public List<string> Target_tag_ { get { return target_tag_; } }
    [SerializeField, Header("MoveArea : 行動範囲")]
    private BoxCollider2D move_lim_boxCol_;
    [SerializeField, Header("移動速度[1マス/s]")]
    private float move_velocity = 1f;
    [SerializeField, Header("RigidBody2D")]
    private Rigidbody2D rb2d_;
    [SerializeField, Header("壁判定")]
    private EnemyWall Wall_check_;
    [SerializeField, Header("索敵判定")]
    private EnemySearch Target_seartch_;

    
    private Vector2[] move_limits_ = new Vector2[2];
    private int dir_ = -1;                  // 移動方向
    private Transform target_trans_;        // 攻撃対象のTransformを取得


    enum enemy_mode_
    {
        search, attack, box, melt,
    }
    private enemy_mode_ e_mode = enemy_mode_.search;

    void Start()
    {
        // ----- 初期化 ----- //
        // ---- 行動限界範囲設定(四角形)
        move_limits_ = moveLimits(ref move_lim_boxCol_);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 0. 偵察モード 
        if (enemy_mode_.search == e_mode)
        {
            // ---- 索敵モード初期化 ----- //
            if (Target_seartch_.Is_target_ == true) {
                Target_seartch_.Is_target_ = false;     // 索敵クラス動作開始
                target_trans_ = null;                   // 索敵対象を一旦null
            }
            // ---- 移動＆壁があったら反転 ---- //
            GroundEnemyMove(Time.fixedDeltaTime, ref rb2d_, ref Wall_check_);
            // ---- ターゲットを発見した時：ターゲットのTransform取得＆モード変化( to 1) ---- //
            if(Target_seartch_.Target_trans_ != null)
            {
                target_trans_ = Target_seartch_.Target_trans_;
                e_mode = enemy_mode_.attack;
            }

        // 1. 攻撃モード
        } else if(enemy_mode_.attack == e_mode)
        {
           // ---- ターゲットへ攻撃するため玉生成＆投げつける ---- //

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

    private Vector2[] moveLimits(ref BoxCollider2D limBox)
    {
        Vector2[] limit_rl = new Vector2[2];
        limit_rl[0] = new Vector2(
            move_lim_boxCol_.transform.position.x - move_lim_boxCol_.size.x / 2,
            move_lim_boxCol_.transform.position.y + move_lim_boxCol_.size.y / 2
            );
        limit_rl[1] = new Vector2(
            move_lim_boxCol_.transform.position.x + move_lim_boxCol_.size.x / 2,
            move_lim_boxCol_.transform.position.y - move_lim_boxCol_.size.y / 2
            );
        // MoveArea：以上操作より用無し。消す
        limBox.gameObject.SetActive(false);
        return limit_rl;
    }



}
