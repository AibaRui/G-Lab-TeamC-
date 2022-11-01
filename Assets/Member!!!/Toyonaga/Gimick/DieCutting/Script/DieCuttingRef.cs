using UnityEngine;

public class DieCuttingRef : MonoBehaviour
{
    [SerializeField, Tooltip("雪参照用画像アタッチ ＆ 本スクリプト下部(メンバ変数)のint [,] 配列を 0 で指定して下さい")]
    private Sprite im_snow_;
    [SerializeField, Tooltip("水参照画像アタッチ ＆ 本スクリプト下部(メンバ変数)のint [,] 配列を 1 で指定して下さい")]
    private Sprite im_water_;
    [SerializeField, Tooltip("画像同士の間隔を開ける場合、値を指定")]
    float im_span_ = 0f;

    private static GameObject[,] boxes_;       // 画像ゲームオブジェクト

    // ---- 要編集：雪画像：0, 水画像 : 1で初期化 ---- //
    private int[,] boxes_state_ = new int [4, 4]
    {
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {1, 0, 0, 0, },
        {1, 0, 0, 0, },
    };                       // 参照画像の並びをスクリプトから指定する
    public int[,] Boxes_state { get { return boxes_state_; } }  // getter()

    private void Start()
    {
        // ---- 既に子オブジェクトが存在する場合：削除 --- //
        DestroyChildren(this.gameObject);
        // ---- int[,]配列情報を基に画像配列描画 ---- //
        boxes_ = new GameObject[boxes_state_.GetLength(0), boxes_state_.GetLength(1)];
        for (var r = 0; r < boxes_state_.GetLength(0); r++)
        {
            for (var c = 0; c < boxes_state_.GetLength(1); c++)
            {
                boxes_[r, c] = new GameObject($"Box({r},{c})");
                boxes_[r, c].transform.parent = transform;                      // 子オブジェクトにする
                var sprite = boxes_[r, c].AddComponent<SpriteRenderer>();       // 画像表示のためレンダーを与える
                if (boxes_state_[r, c] == 0) { sprite.sprite = im_snow_; }      // 画像を与える
                else if (boxes_state_[r, c] == 1) { sprite.sprite = im_water_; }
                else { Debug.Log("Error : 雪 or 水画像は 0 or 1 の整数で指定して下さい"); }
                boxes_[r, c].transform.position = new Vector3(                  // 親座標からの移動距離指定
                    transform.position.x + c * (sprite.size.x + im_span_),
                    transform.position.y - r * (sprite.size.y + im_span_),
                    transform.position.z);
            }
        }
    }

    private void DestroyChildren(GameObject parent)
    {
        for(var i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            child.parent = null;
            GameObject.Destroy(child.gameObject);
        }
    }

   
}
