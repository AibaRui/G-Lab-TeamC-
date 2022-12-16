using UnityEngine;

public class GeyserManager : GimickBase
{
    [SerializeField, Header("炎オーラの名前を登録")]
    private string fire_aura_;
    public string Fire_aura { get { return fire_aura_; } }
    [SerializeField, Header("氷オーラの名前を登録")]
    private string ice_aura_;
    public string Ice_aura { get { return ice_aura_; } }
    [SerializeField, Header("ポーズ中、Geyserの動きを全て止める")]
    public bool is_Pause_ = false;
    [SerializeField, Header("氷の岩が溶けたかどうか")]
    public bool is_IceRock_melted_ = false;
    [SerializeField, Header("プレイヤー1タグ名")]
    private string player1_name;
    [SerializeField, Header("プレイヤー2タグ名")]
    private string player2_name;
    [SerializeField, Header("オーディオソース1")]
    private AudioSource ad1;
    [SerializeField, Header("オーディオソース2")]
    private AudioSource ad2;
    [SerializeField]
    private bool is_SE_active = false;
    [SerializeField, Header("氷が溶ける音")]
    private AudioClip iceMelt_sound;
    [SerializeField, Header("氷が固まる音")]
    private AudioClip iceMake_sound;
    [SerializeField, Header("間欠泉が湧く音")]
    private AudioClip water_gush_sound;
    [SerializeField, Header("間欠泉が吹き出す音")]
    private AudioClip water_spewOut_sound;

    public enum  se_fountain{
        normal, 
        spewing,
    }
    private se_fountain se_fo = se_fountain.normal;
    private se_fountain se_fo_pre = se_fountain.normal;  // 1フレーム前の再生SE格納

    public enum se_ice
    {
        none,
        ice_making,
        ice_melting,
    }
    private se_ice se_ic = se_ice.none;
    private se_ice se_ic_pre = se_ice.none;     // 1フレーム前の再生SE格納 


    private void Update()
    {
        // ---- SE管理 ---- //
        if (!is_SE_active) { return; }  // プレイヤーが範囲内に居ない：SE無効
        // 1. 噴水SE管理
        switch (se_fo)
        {
            case se_fountain.normal:
                //if (se_fo != se_fo_pre) { ad1.Stop(); }   // 湧きだし音は常に再生
                if (ad1.isPlaying) { break; }
                ad1.PlayOneShot(water_gush_sound);
                break;

            case se_fountain.spewing:
                if (se_fo != se_fo_pre) { ad1.Stop(); }     // 1フレーム前の設定SEと違う：停止
                if (ad1.isPlaying) { break; }
                ad1.PlayOneShot(water_spewOut_sound);
                break;
        }
        se_fo_pre = se_fo;                                  // 1フレーム前の設定SEを格納

        // 2. 氷SE管理
        switch (se_ic)
        {
            case se_ice.none:
                
                break;

            case se_ice.ice_making:
                if (se_ic != se_ic_pre) { break; }
                if (ad2.isPlaying) { break; }
                ad2.PlayOneShot(iceMake_sound);
                se_ic = se_ice.none;
                break;

            case se_ice.ice_melting:
                if (se_ic != se_ic_pre) { break; }
                if (ad2.isPlaying) { break; }
                ad2.PlayOneShot(iceMelt_sound);
                se_ic = se_ice.none;
                break;
        }
        se_ic_pre = se_ic;                                  // 1フレーム前の設定SEを格納


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == player1_name || collision.gameObject.tag == player2_name)
        {
            is_SE_active = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player1_name || collision.gameObject.tag == player2_name)
        {
            is_SE_active = false;
        }
    }

    public void SetFountainSE(se_fountain se_f)
    {
        // ---- 噴水のSEのタイプを変更 ---- //
        se_fo = se_f;
    }

    public void SetIceSE(se_ice se_i)
    {
        // ---- 氷のSEのタイプ変更 ---- //
        se_ic = se_i;
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
