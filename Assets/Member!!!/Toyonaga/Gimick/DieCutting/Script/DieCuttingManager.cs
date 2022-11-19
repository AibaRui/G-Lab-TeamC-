using UnityEditor;
using UnityEngine;

public class DieCuttingManager : GimickBase
{
    [SerializeField, Header("ポーズ中は本ギミック無効")]
    public bool is_Pause_ = false;
    [SerializeField, Header("完了フラグ：ギミック解いたらTrueとなる")]
    public bool is_complete_ = false;
    [SerializeField, Header("お手本の雪画像を登録")]
    private Sprite snow_ref_img_;
    [SerializeField, Header("お手本の水画像を登録")]
    private Sprite water_ref_img_;
    [SerializeField, Header("ギミック雪画像を登録")]
    private Sprite snow_img_;
    [SerializeField, Header("ギミック水画像を登録")]
    private Sprite water_img_;
    [SerializeField, Header("炎オーラ名を入力")]
    private string fire_tag_name_;
    [SerializeField, Header("氷オーラ名を入力")]
    private string ice_tag_name_;
    [SerializeField, Header("お手本ブロック間隔を指定")]
    private float ref_box_span_ = 0f;
    [SerializeField, Header("ギミックブロック間隔を指定")]
    private float gim_box_span_ = 0f;
    [SerializeField, Header("ブロックの状態変化時間を設定")]
    private float box_change_time_ = 1f;
    [SerializeField, Header("お手本ブロック表示位置ゲームオブジェクト登録")]
    private GameObject ref_pos_;
    [SerializeField, Header("お手本ブロックのゲームオブジェクト登録")]
    private GameObject ref_box_;
    [SerializeField, Header("ギミックブロック表示位置ゲームオブジェクト登録")]
    private GameObject gim_pos_;
    [SerializeField, Header("ギミックブロックのゲームオブジェクト登録")]
    private GameObject gim_box_;

    private struct Gim_boxes_
    {
        // --- ギミックボックス：構造体で楽に管理したい --- //
        public GameObject gim_box_;           // ギミックブロックゲームオブジェクト参照先を格納
        public DieCuttingGimBox gim_script_;    // ギミックブロックスクリプトの参照先格納
    }
    Gim_boxes_[,] gim_boxes_st_;

    // ------------------------------------------------//
    // ---- 要編集：雪画像：0, 水画像 : 1で初期化 ---- //
    // --- 参照ブロックの初期状態を定義しておく --- //
    private int[,] ref_state_ = new int[4, 4]
    {
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {1, 0, 0, 0, },
        {1, 0, 0, 0, },
    };                    
    // --- ギミックブロックの初期状態を定義しておく --- //
    // 注：必ずref_state_と同じサイズで定義する事。
    private int[,] gim_state_ = new int[4, 4]
    {
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
    };
    // ------------------------------------------------//

    private void Start()
    {
        // ----- お手本ブロック, ギミックブロック生成 ----- //
        GenerateRefBoxes(ref ref_state_, ref ref_pos_, ref ref_box_, ref snow_ref_img_, ref water_ref_img_, ref_box_span_);
        GenerateGimBoxes(ref gim_state_, ref gim_boxes_st_, ref gim_pos_, ref gim_box_, ref snow_img_, ref water_img_, gim_box_span_);
    }

    private void FixedUpdate()
    {
        // ----- ギミックブロックと参照ブロックの状態比較: 合致すればis_complete = true ----- //
        if (is_Pause_) { return; }           // ポーズ中は動作無し
        if (is_complete_) { return; }       // 一度でもギミック成功：ギミック動作不要
        // ---- 状態比較 ---- //
        bool stateChanged = false;
        for (var r = 0; r < gim_boxes_st_.GetLength(0); r++)
        {
            for(var c = 0; c < gim_boxes_st_.GetLength(1); c++)
            {
                if (gim_boxes_st_[r, c].gim_script_.Is_state_changed)
                {
                    stateChanged = true;
                    break;
                }
                if (stateChanged) { break; }
            }
        }
        // ---- ボックスの状態が1ブロックでも変化があった場合：状態比較 ---- //
        if (!stateChanged) { return; }
        bool isMatched = true;
        for(var r = 0; r < gim_boxes_st_.GetLength(0); r++)
        {
            for(var c = 0; c < gim_boxes_st_.GetLength(1); c++)
            {
                if(ref_state_[r,c] != gim_boxes_st_[r, c].gim_script_.State) { isMatched = false; break; }
            }
            if(!isMatched) { break; }
        }
        if (isMatched) { is_complete_ = true; }
    }


    private void GenerateRefBoxes(ref int[,] ref_a, ref GameObject pos_obj, ref GameObject gen_box, ref Sprite snow, ref Sprite water, float span)
    {
        // ---- 参照ブロックを一定間隔で生成していく ---- //
        // --- 初期化 --- //
        DestroyChildren(pos_obj);   // 生成位置参照ゲームオブジェクトの子を一旦削除
        for(var r = 0; r < ref_a.GetLength(0); r++)
        {
            for(var c = 0; c < ref_a.GetLength(1); c++)
            {
                // -- ブロック生成 -- //
                var box = Instantiate(gen_box);
                box.name = $"RefBox({r}, {c})";
                box.transform.parent = pos_obj.transform;
                var spriteR = box.GetComponent<SpriteRenderer>();
                if (ref_a[r, c] == 0) { spriteR.sprite = snow; }
                if(ref_a[r, c] == 1) { spriteR.sprite = water; }
                box.transform.position = new Vector3(
                    pos_obj.transform.position.x + c * (spriteR.size.x + span),
                    pos_obj.transform.position.y - r * (spriteR.size.y + span),
                    pos_obj.transform.position.z
                    );
                // ※ 参照ブロックは描画するだけなので参照等は格納無し

            }
        }
    }
   
    private void GenerateGimBoxes(ref int[,] ref_a, ref Gim_boxes_[,] g_boxes, ref GameObject pos_obj, ref GameObject gen_box, ref Sprite snow, ref Sprite water, float span)
    {
        // ---- ギミックブロックを一定間隔で生成していく ---- //
        // --- 初期化 --- //
        DestroyChildren(pos_obj);   // 生成位置参照ゲームオブジェクトの子を一旦削除
        g_boxes = new Gim_boxes_[ref_a.GetLength(0), ref_a.GetLength(1)];       // ゲームブロック構造体初期化
        for(var r = 0; r < g_boxes.GetLength(0); r++)
        {
            for(var c = 0; c < g_boxes.GetLength(1); c++)
            {
                // -- ブロック生成 -- //
                var box = Instantiate(gen_box);
                box.name = $"GimBox({r},{c})";
                box.transform.parent = pos_obj.transform;
                var spriteR = box.GetComponent<SpriteRenderer>();
                if(ref_a[r,c] == 0) { spriteR.sprite = snow; }
                if(ref_a[r,c] == 1) { spriteR.sprite = water; }
                box.transform.position = new Vector3(
                    pos_obj.transform.position.x + c * (spriteR.size.x + span),
                    pos_obj.transform.position.y - r * (spriteR.size.y + span),
                    pos_obj.transform.position.z
                    );
                // -- 参照を格納 -- //
                g_boxes[r, c].gim_box_ = box;
                if(box.GetComponent<DieCuttingGimBox>() != null)
                g_boxes[r,c].gim_script_ = box.GetComponent <DieCuttingGimBox>();
                
                // -- setter -- //
                var scr = g_boxes[r, c].gim_script_;
                scr.State = ref_a[r, c];
                scr.Snow_img = snow_img_;
                scr.Water_img = water_img_;
                scr.Fire_tag_name = fire_tag_name_;
                scr.Ice_tag_name = ice_tag_name_;
                scr.Box_change_time = box_change_time_;
            }
        }
    }

    private void DestroyChildren(GameObject parent)
    {
        for (var i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            child.parent = null;
            GameObject.Destroy(child.gameObject);
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
