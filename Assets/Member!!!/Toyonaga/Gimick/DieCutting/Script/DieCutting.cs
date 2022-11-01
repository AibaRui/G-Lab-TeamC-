using UnityEngine;

public class DieCutting : MonoBehaviour
{
    [SerializeField, Tooltip("DieCuttingRefゲームオブジェクトを登録")]
    private DieCuttingRef dc_ref_;
    [SerializeField, Tooltip("本ギミック攻略時、有効化するゲームオブジェクトを登録")]
    private GameObject awake_object_;
    [SerializeField, Tooltip("表示する雪画像を登録")]
    private Sprite im_snow_;
    [SerializeField, Tooltip("表示する水画像を登録")]
    private Sprite im_water_;
    [SerializeField, Tooltip("画像同士の間隔を開ける場合、値を指定")]
    float im_span_ = 0f;
    [SerializeField, Tooltip("参照とブロックの状態が揃った時：trueとなる(getter用意済)")]
    bool is_complete_ = false;
    [SerializeField, Tooltip("炎オーラのタグ名を指定して下さい")]
    private string fire_Tag_name_ = "P1";
    [SerializeField, Tooltip("氷オーラのタグ名を指定して下さい")]
    private string ice_Tag_name_ = "P2";
    [SerializeField, Tooltip("boxがオーラによって状態変化するまでの時間を指定して下さい")]
    private float box_change_time_ = 1.0f;
    public bool Is_complete { get { return is_complete_; } }
    

    private static GameObject[,] boxes_;    // 画像ゲームオブジェクト
    private DieCuttingBox[,] boxes_script_; // 画像ゲームオブジェクトのスクリプト取得
    private int[,] boxes_state_ref_;        // 参照との一致状態比較用配列
    private int[,] boxes_state_;            // 本DieCuttingの現在の状態を格納(デフォルト：雪)    

    void Start()
    {
        // ---- 参照オブジェクトの一致状態比較用の配列を取得する ---- //
        boxes_state_ref_ = dc_ref_.Boxes_state;
        boxes_state_ = new int[boxes_state_ref_.GetLength(0), boxes_state_ref_.GetLength(1)];   // 配列サイズを参照と揃える
        // ---- 既に子オブジェクトが存在する場合：削除 --- //
        DestroyChildren(this.gameObject);
        // ---- int[,]配列情報を基に画像配列描画 ---- //
        boxes_ = new GameObject[boxes_state_.GetLength(0), boxes_state_.GetLength(1)];
        boxes_script_ = new DieCuttingBox[boxes_state_.GetLength(0), boxes_state_.GetLength(1)];
        for (var r = 0; r < boxes_state_.GetLength(0); r++)
        {
            for (var c = 0; c < boxes_state_.GetLength(1); c++)
            {
                // --- boxの状態を雪(0)で初期化 --- //
                boxes_state_[r, c] = 0;                                     // 水で初期化したい場合、1とする
                boxes_[r, c] = new GameObject($"Box({r},{c})");
                boxes_[r, c].transform.parent = transform;                  // 子オブジェクトにする
                // -- 画像設定 -- //
                var sprite = boxes_[r, c].AddComponent<SpriteRenderer>();   // 画像表示のためレンダーを与える
                if (boxes_state_[r, c] == 0) { sprite.sprite = im_snow_; }  // 画像を与える
                else if (boxes_state_[r, c] == 1) { sprite.sprite = im_water_; }
                else { Debug.Log("Error : 雪 or 水画像は 0 or 1 の整数で指定して下さい"); }
                // -- スクリプト＆メンバ変数を与える設定 -- //
                var script = boxes_[r, c].AddComponent<DieCuttingBox>();
                script.Ice_Tag_name = ice_Tag_name_;                        // オーラと比較するタグ名を与える
                script.Fire_Tag_name = fire_Tag_name_;                      // 
                script.State = 0;                                           // 水で初期化したい場合、１とする
                script.SetSnowImg = im_snow_;
                script.SetWaterImg = im_water_;
                script.ChangeTime = box_change_time_;                       // 
                boxes_script_[r, c] = script;                               // スクリプトの参照取得
                // -- コライダー設定 -- //
                var collider = boxes_[r, c].AddComponent<BoxCollider2D>();  // 当たり判定用にコライダーを与える
                collider.size = new Vector2(sprite.size.x, sprite.size.y);  // コライダーサイズを画像サイズに揃える
                boxes_[r, c].transform.position = new Vector3(              // 親座標からの移動距離指定
                    transform.position.x + c * (sprite.size.x + im_span_),
                    transform.position.y - r * (sprite.size.y + im_span_),
                    transform.position.z);
                
            }
        }
    }

    void Update()
    {
        // ---- 一度でもコンプリートフラグが立ったら、本ギミックは動作不要
        if (is_complete_) { return; };
        // ---- boxの状態変化(bool is_type_changed)検知の度、boxes_state情報アップデート ---- //
        bool stateChanged = false;
        for (var r = 0; r < boxes_.GetLength(0); r++)
        {
            for (var c = 0; c < boxes_.GetLength(1); c++ )
            {
                if (boxes_script_[r, c].IsStateChanged)
                {
                    boxes_state_[r, c] = boxes_script_[r, c].State;
                    stateChanged = true;
                }
            }
        }
        if (stateChanged)
        {
            // --- ref : boxes_state_refと現在の状態を比較 --- //
            bool isMached = true;
            for(var r = 0; r < boxes_state_ref_.GetLength(0); r++)
            {
                for(var c = 0; c < boxes_state_ref_.GetLength(1); c++)
                {
                    if(boxes_state_ref_[r,c] != boxes_state_[r, c]) { isMached = false; break; }
                }
            }
            // --- 参照とブロックの状態が揃った時：コンプリートフラグを立てる --- //
            if (isMached) { is_complete_ = true; }
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
}
