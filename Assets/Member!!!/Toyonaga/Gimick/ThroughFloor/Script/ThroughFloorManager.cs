using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughFloorManager : MonoBehaviour
{
    [Header("--- 注意：本スクリプトから画像＆コライダサイズ一括変更 ---")]
    [SerializeField, Header("すりぬけ床の横幅サイズ指定")]
    private float width_ = 2.0f;
    [SerializeField, Header("すりぬけ床の縦幅サイズ指定")]
    private float height_ = 0.2f;
    [SerializeField, Header("すりぬけ床画像登録")]
    private SpriteRenderer through_floor_;
    [SerializeField, Header("すりぬけ床コライダ登録")]
    private BoxCollider2D box_collider_;
    
    private void OnValidate()
    {
        // ----- インスペクタビューの値更新毎に関数実施 ----- //
        // ---- スクリプトから画像＆コライダのサイズ調整 ---- //
        through_floor_.size = new Vector2(width_, height_);
        box_collider_.size = new Vector2(width_, height_);
    }
}
