using UnityEngine;

public class DieCuttingGimBox : GimickBase
{
    // ----- ギミックブロックの状態等を管理する ----- //
    [SerializeField]
    private bool is_Pause_ = false;
    private bool is_state_changed_ = false;
    public bool Is_state_changed { get { return is_state_changed_; } }
    private int state_;
    public int State { get { return state_; } set { state_ = value; } }
    private Sprite snow_img_;
    public Sprite Snow_img { get { return snow_img_; } set { snow_img_ = value; } }
    private Sprite water_img_;
    public Sprite Water_img { get { return water_img_; } set { water_img_ = value; } }
    private string fire_tag_name_;
    public string Fire_tag_name { get { return fire_tag_name_; } set { fire_tag_name_ = value; } }
    private string ice_tag_name_;
    public string Ice_tag_name { get { return ice_tag_name_; } set { ice_tag_name_ = value; } }
    private float box_change_time_ = 1f;
    public float Box_change_time { get { return box_change_time_; } set { box_change_time_ = value;} }
    
    SpriteRenderer Sp;
    private bool is_other_enterd_ = false;  // 他のコライダーが侵入中かどうかの判定
    private float count_snow_ = 0;          // オーラの影響時間(炎)をカウント
    private float count_water_ = 0;         // オーラの影響時間(氷)をカウント

    private void Start()
    {
        Sp = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // ---- ポーズ中は動作無し ---- //
        if (is_Pause_) { return; }
        // ---- オーラの検知 ---- //
        if (!is_other_enterd_)
        {
            if (count_snow_ > 0) { count_water_ -= Time.fixedDeltaTime; }
            if(count_water_ > 0) { count_snow_ -= Time.fixedDeltaTime; }
        }
        // ---- 状態変化フラグを検知：画像変更後、フラグをリセット ---- //
        if (!is_state_changed_) { return; } // 状態変化が無ければ終了
        if (state_ == 0) { Sp.sprite = snow_img_; }
        else if (state_ == 1) { Sp.sprite = water_img_; }
        is_state_changed_ = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ---- ボックスの状態(氷/水)を変更 ---- //
        // ---- ポーズ中は動作無し ---- //
        if (is_Pause_) { return; }
        // ---- 状態変更 ---- //
        is_other_enterd_ = true;
        if (other.transform.CompareTag(fire_tag_name_) && state_ == 0)
        {
            count_water_ += Time.deltaTime;
            if (count_water_ >= box_change_time_)
            {
                count_water_ = 0;
                is_state_changed_ = true;
                state_ = 1;
            }
        }
        if (other.transform.CompareTag(ice_tag_name_) && state_ == 1)
        {
            count_snow_ += Time.deltaTime;
            if(count_snow_ >= box_change_time_)
            {
                count_snow_ = 0;
                is_state_changed_ = true;
                state_ = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ---- ボックス判定内にオブジェクトの有無を確認 ---- //   
        // ---- ポーズ中は動作無し ---- //
        if (is_Pause_) { return; }
        is_other_enterd_ = false;
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
