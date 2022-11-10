using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBase : MonoBehaviour
{
    [SerializeField, Header("GeyserManager登録")]
    private GeyserManager manager_;
    [SerializeField, Header("FountainScript登録")]
    private Fountain fountain_;
    private float small_vibration_time_ = 1f;
    private float small_vibration_mag_ = 5f;
    private float transform_rate_ = 0f;
    private float init_size_ = 0.01f;
    private Vector3 max_size_ = new Vector3(0, 0, 0);

    private void Start()
    {
        // ----- 初期化：getterでパラメータ取得 ----- //
        small_vibration_time_ = fountain_.Small_vibration_time_;
        small_vibration_mag_ = fountain_.Small_vibration_mag_;
        transform_rate_ = fountain_.TransformRate_;
        init_size_ = fountain_.Init_size_;
        max_size_ = transform.localScale;
        // ---- 初期設定 ---- //
        transform.localScale = new Vector3(1*init_size_, 1*init_size_, transform.localScale.z);       // 微小サイズにしておく
    }

    private void FixedUpdate()
    {
        if (manager_.is_Pause_) { return; }             // ポーズ中はスクリプト無効
        float rad = small_vibration_mag_ * Mathf.Sin(Time.time * (1 / (small_vibration_time_ / 2)));    // 傾斜角度決定
        smallVib(ref rad, Time.deltaTime);      // ブルブル震える処理
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (manager_.is_Pause_) { return; }             // ポーズ中はスクリプト無効
        if (!manager_.is_IceRock_melted_) { return; }   // IceRockが溶けきるまでスクリプト無効
        // ----- オーラを受けた時の処理 ----- //
        // ---- 炎 ---- //
        if (other.transform.CompareTag(manager_.Fire_aura))
        {
            if (init_size_ > transform.localScale.x) { return; }
            transform.localScale -= new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0
            );
        }
        else if (other.transform.CompareTag(manager_.Ice_aura))
        // ---- 氷 ---- //
        {
            if (max_size_.x < transform.localScale.x) { return; }
            transform.localScale += new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0
            );
        }
    }

    private void smallVib(ref float rad, float delta_time)
    {
        transform.localEulerAngles = new Vector3(0, 0, rad);
    }

}
