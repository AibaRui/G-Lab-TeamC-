using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRock : MonoBehaviour
{
    [SerializeField, Header("GeyserManager登録")]
    GeyserManager manager_;
    [SerializeField, Header("IceRockのRigidBody2D登録")]
    Rigidbody2D rb_;
    [SerializeField, Header("IceRockの画像をもつゲームオブジェクト登録")]
    GameObject iceRockObj_;
    [SerializeField, Header("小刻みに揺れる間隔調整")]
    private float small_vibration_time_ = 1f;
    [SerializeField, Header("小刻みに揺れる角度(deg)")]
    private float small_vibration_mag_ = 20;
    [SerializeField, Header("IceRockのサイズが変わる率指定")]
    private float transform_rate_ = 1.0f;

    private float init_scaleX = 1f;     // 初期状態のIceRockGameObjectのサイズ登録
    private float init_scaleY = 1f;

    private void Start()
    {
        // ----- iceRockの初期サイズ格納 ----- //
        init_scaleX = iceRockObj_.transform.localScale.x;
        init_scaleY = iceRockObj_.transform.localScale.y;
    }

    private void FixedUpdate()
    {
        // ----- ポーズ中は動きを停止 ----- //
        if (manager_.is_Pause_){ 
            rb_.constraints = RigidbodyConstraints2D.FreezeAll;

            return;
        } else {
            // 解除
            rb_.constraints = RigidbodyConstraints2D.None;
            rb_.constraints = RigidbodyConstraints2D.FreezePositionX;   // x軸は拘束
        }
        // ----- 通常動作 ----- //
        // --- 初期化 --- //
        float rad = small_vibration_mag_ * Mathf.Sin(Time.time * (1/(small_vibration_time_/2)));
        smallVib(ref rad, Time.deltaTime);      // ブルブル震える処理

    }

    private void smallVib(ref float rad, float delta_time)
    {
        iceRockObj_.transform.localEulerAngles = new Vector3(0, 0, rad);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ----- オーラを受けた時の処理 ----- //
        // ---- 炎 ---- //
        if (other.transform.CompareTag(manager_.Fire_aura))
        {
            transform.localScale -= new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            iceRockObj_.transform.localScale -= new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            //  --- SE管理 --- //
            manager_.SetIceSE(GeyserManager.se_ice.ice_melting);

        } else if (other.transform.CompareTag(manager_.Ice_aura))
        // ---- 氷 ---- //
        {
            if(init_scaleX < transform.localScale.x) { return; }    // 大きさ制限
            transform.localScale += new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            iceRockObj_.transform.localScale += new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            // --- SE管理 --- //
            manager_.SetIceSE(GeyserManager.se_ice.ice_making);
        }
        // ----- 一定以上のサイズに小さくなった時：本ゲームオブジェクト無効化 ---- //
        // ---- マネージャーにIceRockが削除された事を伝える ---- //
        // ---- 画像ゲームオブジェクトも無効化 ---- //
        if(transform.localScale.x < init_scaleX / 5)
        {
            manager_.is_IceRock_melted_ = true;
            this.gameObject.SetActive(false);
            iceRockObj_.SetActive(false);
        }

    }


}
