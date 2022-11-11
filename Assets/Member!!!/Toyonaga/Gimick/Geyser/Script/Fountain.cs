using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField, Header("小刻みに揺れる間隔調整")]
    private float small_vibration_time_ = 1f;
    public float Small_vibration_time_ { get { return small_vibration_time_; } }
    [SerializeField, Header("小刻みに揺れる角度(deg)")]
    private float small_vibration_mag_ = 5f;
    public float Small_vibration_mag_ { get { return small_vibration_mag_;} }
    [SerializeField, Header("IceRockのサイズが変わる率指定")]
    private float transform_rate_ = 1.0f;
    public float TransformRate_ { get { return transform_rate_; } }
    [SerializeField, Header("IceBaseの初期サイズ")]
    private float init_size_ = 0.01f;
    public float Init_size_ { get { return init_size_; } }
    public float InitSize_ { get { return init_size_; } }
    private Vector3 max_size_ = new Vector3(0, 0, 0);       // FountainIceBaseの初期サイズ

    private void Start()
    {
        // ----- 初期値取得 ----- //
        height_max_ = fountain_heightObj_.transform.localPosition.y;    // 噴水高さ限界
        max_size_ = fountain_baseObj_.transform.localScale;             // IceBaseの初期サイズ取得
        // ----- 初期設定 ----- //
        fountain_baseObj_.transform.localScale = new Vector3(1, 1, 1) * init_size_;     // IceBaseを見えないくらい小さくする
        fountain_heightObj_.SetActive(false);                           // IceBase高さ限界を見えなくする

    }

    private void FixedUpdate()
    {
        if (manager_.is_Pause_) { return; }             // ポーズ中はスクリプト無効
        if (!manager_.is_IceRock_melted_) { return; }   // IceRockが溶けきるまでスクリプト無効

    }

  

}

