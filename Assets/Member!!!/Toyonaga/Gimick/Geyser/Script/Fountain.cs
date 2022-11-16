using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ----- 噴水の動き・足場の動きを管理するクラス ----- //
// ※足場(fountain_baseObj)の細かい挙動(揺れる / オーラを受けて大きくなる/小さくなる)は、IcsBase.csに任せる。
// メイン：氷塊(噴水に蓋をしているオブジェクト）が消えれば動作。
//  -> 噴水画像Object(fountain_imgObj)と足場(fountain_baseObj)を周期的に上下させ、
//     間欠泉が吹き出す / 止む動きを本クラスで管理している。
//     FountainMove()関数でその動きを実現している

public class Fountain : MonoBehaviour
{
    [SerializeField, Header("GeyserManager登録")]
    private GeyserManager manager_;
    [SerializeField, Header("噴水画像ゲームオブジェクト登録")]
    private GameObject fountain_imgObj_;
    [SerializeField, Header("氷の足場ゲームオブジェクト登録")]
    private GameObject fountain_baseObj_;
    [SerializeField, Header("噴水の高さ限界を示すゲームオブジェクト登録")]
    private GameObject fountain_heightObj_;
    private float height_max_;              // 噴水高さ限界
    private float fountain_height_;         // 噴水の高さを決定
    private float fountain_init_height = 1.0f;  // 噴水の初期高さを取得
    public float Fountain_height_ { get { return fountain_height_; } }
    [SerializeField, Header("噴水がON/OFFする周期の秒数指定(s)")]
    private float fountain_time_ = 5f;
    [SerializeField, Header("噴水の速度を指定(高さ(1) / s)")]
    private float fountain_velocity_ = 3f;
    private float fountain_counter_ = 0f;   // 噴水ON/OFFの時間をカウント
    private bool up = false;                // 噴水上げ/下げフラグ
    public float Fountain_velocity_ { get { return fountain_velocity_; } }
    [SerializeField, Header("氷塊が小刻みに揺れる間隔調整")]
    private float small_vibration_time_ = 1f;
    public float Small_vibration_time_ { get { return small_vibration_time_; } }
    [SerializeField, Header("氷塊が小刻みに揺れる角度(deg)")]
    private float small_vibration_mag_ = 5f;
    public float Small_vibration_mag_ { get { return small_vibration_mag_;} }
    [SerializeField, Header("IceRockのサイズが変わる率指定")]
    private float transform_rate_ = 1.0f;
    public float TransformRate_ { get { return transform_rate_; } }
    [SerializeField, Header("IceBaseの初期サイズ")]
    private float init_size_ = 0.01f;
    private float init_box_height = 0.25f;              // iceBox(足場）の初期位置を取得
    public float Init_size_ { get { return init_size_; } }
    public float InitSize_ { get { return init_size_; } }
    private Vector3 max_size_ = new Vector3(0, 0, 0);       // FountainIceBaseの初期サイズ

    private void Awake()
    {
        
    }
    private void Start()
    {
        // ----- 初期値取得 ----- //
        height_max_ = fountain_heightObj_.transform.localPosition.y;    // 噴水高さ限界
        max_size_ = fountain_baseObj_.transform.localScale;             // IceBaseの初期サイズ取得
        // ----- 初期設定 ----- //
        fountain_baseObj_.transform.localScale = new Vector3(1, 1, 1) * init_size_;         // IceBaseを見えないくらい小さくする
        init_box_height = fountain_baseObj_.transform.localPosition.y;  // IceBoxの下限位置を取得
        fountain_baseObj_.SetActive(false);                             // IceBaseを無効化(氷塊が消えるまで)
        height_max_ = fountain_heightObj_.transform.localPosition.y;    // IceBase＆噴水画像の高さ限界取得
        fountain_heightObj_.SetActive(false);                           // IceBase高さ限界を見えなくする
        fountain_init_height = fountain_baseObj_.GetComponent<SpriteRenderer>().size.y; // 噴水画像の初期位置を取得
        
        
    }

    private void FixedUpdate()
    {
        if (manager_.is_Pause_) { return; }             // ポーズ中はスクリプト無効
        if (!manager_.is_IceRock_melted_) { return; }   // IceRockが溶けきるまでスクリプト無効
        if(fountain_baseObj_.activeSelf == false) { fountain_baseObj_.SetActive(true); }    // 足場の有効化
        FountainMove(height_max_, fountain_velocity_, Time.fixedDeltaTime, ref fountain_imgObj_, ref fountain_baseObj_);

    }

    private void FountainMove(float height, float velocity, float delta_time, ref GameObject fountainImg, ref GameObject fountainBaseObj)
    {
        // ----- 噴水の高さ＆足場の位置を更新 ----- //
        float height_delta = fountain_velocity_ * delta_time;
        // 噴水を上昇させるフラグ
        if (!up)
        {
            // ---- フラグ管理 ---- //
            fountain_counter_ += delta_time;
            if (fountain_counter_ > fountain_time_) { up = true; }
            // --- 足場の処理 ---- //
            if (fountain_baseObj_.transform.localPosition.y >= init_box_height)
            {
                // 下降限界まで移動
                fountain_baseObj_.transform.position -= new Vector3(0, height_delta, 0);
            }
            // --- 噴水画像の処理 ---- //
            if (fountain_imgObj_.transform.localPosition.y >= fountain_init_height/2)
            {
                var img = fountainImg.GetComponent<SpriteRenderer>();
                img.size -= new Vector2(0, height_delta);
                fountainImg.transform.localPosition -= new Vector3(0, height_delta / 2, 0);
            }
        }
        if (up)
        {
            // ---- フラグ管理 ---- //
            fountain_counter_ -= delta_time;
            if (fountain_counter_ < 0) { up = false; }
            // --- 足場の処理 ---- //
            // 高さ限界が来たら停止
            if (fountain_baseObj_.transform.localPosition.y < height)
            {
                
                fountain_baseObj_.transform.position += new Vector3(0, height_delta, 0);
            }
            // --- 噴水画像の処理 --- //
            // 高さ限界が来たら停止
            if (fountain_imgObj_.transform.localPosition.y < height/2)
            {
                var img = fountainImg.GetComponent<SpriteRenderer>();
                img.size += new Vector2(0, height_delta);
                fountainImg.transform.localPosition += new Vector3(0, height_delta / 2, 0);
            }

        }


    }
}

