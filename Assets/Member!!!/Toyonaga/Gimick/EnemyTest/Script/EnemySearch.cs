using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : GimickBase
{ 
    [SerializeField, Header("EnemyManager登録")]
    private EnemyManager Em_;
    [SerializeField, Header("索敵範囲(円)コライダ登録")]
    private CircleCollider2D search_area_;
    [SerializeField, Header("索敵の有無")]
    private bool is_target_ = false;
    public bool Is_target_ { get { return is_target_; } set { is_target_ = value; } }
    [SerializeField, Header("最初に発見したターゲットのTransformを格納する")]
    private Transform target_transform_;
    public Transform Target_trans_ { get { return target_transform_; } }

    private bool is_Pause_ = false;
    private List<string>target_tag_;
   

    private void Start()
    {
        // ----- 初期化 ----- //
        // ---- 索敵対象取得 ---- //
        target_tag_ = Em_.Target_tag_;
    }

    private void FixedUpdate()
    {
        Debug.Log(target_transform_.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ---- 索敵対象が範囲内に入った時：ターゲット発見フラグ有効 ----- //
        // --- 索敵完了後(is_target = true)、外部からフラグを折らない限り再索敵しない --- //
        // --- ポーズ中も動作無し --- //
        if (is_Pause_) { return; }
        if (is_target_) { return; }
        for (var i = 0; i < target_tag_.Count; i++)
        {
            if (other.CompareTag(target_tag_[i]))
            {
                // --- ターゲットのTransform参照値取得 --- //
                is_target_ = true;
                target_transform_ = other.transform;
            
            }
        }
    }

   

    public override void GameOverPause()
    {
        is_Pause_ = true;
    }

    public override void Pause()
    {
        is_Pause_ = true;
    }

    public override void Resume()
    {
        is_Pause_ = false;
    }

}
