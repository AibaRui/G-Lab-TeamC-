using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField, Tooltip("初期のBOXオーラを指定する(※以下、aura_modeと状態を合わせる事")]
    private GameObject p_boxArea_;
    [SerializeField, Tooltip("初期の円オーラを指定する")]
    private GameObject p_circleArea_;
    [SerializeField, Tooltip("パーティクルを配置しない円の大きさを指定する")]
    private float p_cicle_voidArea_size_;
    [SerializeField, Tooltip("生成するパーティクルオブジェクトを指定")]
    private GameObject p_object_;
    private ParticleMove[,] p_obj_scripts_;
    [SerializeField, Tooltip("パーティクルの列の数")]
    private int p_row_ = 5;
    [SerializeField, Tooltip("パーティクルの行の数")]
    private int p_col_ = 2;
    [SerializeField, Tooltip("パーティクルをどれだけばらつきを与えるかの係数(0 ~ 1(%)")]
    private float rand_coff_ = 0.5f;
    [SerializeField, Tooltip("パーティクルをばらつかせる時間間隔を設定(s)")]
    private float rand_time_ = 0.2f;
    private float rand_time_counter_ = 0;
    private enum mode_
    {
        circle, front_box, up_box, back_box, down_box, end
    }
    [SerializeField, Tooltip("本モードを指定する事で、オーラ形状に応じてパーティクルを移動させる")]
    private mode_ aura_mode_ = mode_.up_box;
    private mode_ current_aura_;            // 現在のモード格納
    private mode_ pre_aura_;                // 1フレーム前のモード格納

    private BoxCollider2D p_boxColl_;
    private CircleCollider2D p_circleColl_;
    private int current_mode_;
    private Vector3 boxArea_d_pos_;         // プレイヤー原点 to パーティクルBox中心までの距離ベクトル取得
    private Vector3[,] p_delta_pos_;        // プレイヤー原点からパーティクルiまでの距離を格納しておく
    private Vector3[,] p_target_pos_world_; // パーティクルの目標位置(ワールド座標) 

    private Quaternion box_rot_;            // プレイヤーから

    private struct particle
    {
        // パーティクル情報を一括管理
        public GameObject particle_;
        public ParticleMove pm_;
        public Vector3 position_;
        public Vector3 delta_position_;
        float rotate_length_;
        int rotate_dir_;
        public Quaternion rot_;
    }
    private particle[,] particle_st_;


    private void Start()
    {
        // ---- 初期化処理 ---- //
        p_boxColl_ = p_boxArea_.GetComponent<BoxCollider2D>();
        p_circleColl_ = p_circleArea_.GetComponent<CircleCollider2D>();
        current_mode_ = (int) aura_mode_;       // オーラの初期状態をインスペクタから取得
        current_aura_ = aura_mode_;             // オーラの現在の状態を格納
        // --- パーティクル生成 --- //
        particle_st_ = GenerateParticles();
        // --- その他、モードチェンジ等に必要なパラメータを取得しておく --- //

        



    }

    private void Update()
    {
        // --- test --- //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            current_aura_ += 1;
            if (current_aura_ == mode_.end) { current_aura_ = mode_.circle; }

        };
        if (ModeChange())
        {
            Debug.Log(current_aura_);
        }

    }

    private void FixedUpdate()
    {
        // ---- 実行中の処理 ---- //


        ParticleRandomUpdate_Box(ref particle_st_, ref current_aura_, Time.fixedDeltaTime);
        // --- パーティクル位置のアップデート --- //
        for (int r = 0; r < p_row_; r++)
        {
            for(int c = 0; c < p_col_; c++)
            {
                
                particle_st_[r, c].pm_.TargetPos = transform.position + particle_st_[r, c].delta_position_;
            }
        }
        
        
    }

    private particle[,] GenerateParticles()
    {
        // ---- パーティクルゲームオブジェクトを範囲内に生成 ---- //
        particle[,] p = new particle[p_row_, p_col_];
        //mode_ temp_mode = md;               // 現在のモードを格納しておく
        current_mode_ = (int)mode_.up_box;  // モードupで初期化処理を行います
        Vector3 box_pos = transform.position - p_boxColl_.transform.position;   // プレイヤー原点からBoxコリジョンまでの距離ベクトル取得
        float box_width = p_boxColl_.size.x;
        float box_height = p_boxColl_.size.y;
        float box_length = box_pos.magnitude;   // 距離情報として格納
        float box_cell_width_ = box_width / p_row_;
        float box_cell_height_ = box_height / p_col_;

        float x0 = p_boxColl_.transform.position.x - box_width / 2;       // boxコライダー左上の座標を基準にパーティクル生成
        float y0 = p_boxColl_.transform.position.y + box_height / 2;

        for(int r = 0; r < p_row_; r++)
        {
            for(int c = 0; c < p_col_; c++)
            {
                // 位置情報決定
                float pos_x = x0 + r * box_cell_width_ + box_cell_width_ / 2;
                float pos_y = y0 - c * box_cell_height_ - box_cell_height_ / 2;
                // 配置にランダム性を導入する
                pos_x += Random.Range(-box_cell_width_/2 * rand_coff_, box_cell_width_/2 * rand_coff_);
                pos_y += Random.Range(-box_cell_height_/2 * rand_coff_, box_cell_height_/2 * rand_coff_);

                // --- ゲームオブジェクトのインスタンス＆情報指定 --- //
                var obj = Instantiate(p_object_);
                obj.transform.parent = transform;   // 親を本ゲームオブジェクトに設定
                obj.transform.position = new Vector3(pos_x, pos_y, transform.position.z);   // boxコライダー指定の位置に移動
                // --- 構造体に情報を格納していく --- //
                p[r, c].particle_ = obj;
                p[r, c].pm_ = obj.GetComponent<ParticleMove>();
                p[r, c].position_ = new Vector3(pos_x, pos_y, transform.position.z);
                p[r, c].delta_position_ = p[r, c].position_  - transform.position;
                p[r, c].rot_ = Quaternion.AngleAxis(Mathf.Deg2Rad * 90, transform.forward);
                p[r,c].pm_.TargetPos = p[r,c].position_;
            }
        }
        return p;
    }

    private void ParticleRandomUpdate_Box(ref particle[,] p, ref mode_ m, float delta_time)
    {
        // ---- パーティクルをランダムに動かす: BoxCollider対象 ---- //
        rand_time_counter_ += delta_time;
        if (rand_time_counter_ < rand_time_) { return; }
        rand_time_counter_ = 0;
        if(m != mode_.front_box && m!= mode_.up_box && m!= mode_.back_box && m!= mode_.down_box) { return; }
        // --- パーティクルの目標位置更新 --- //
        float box_width = p_boxColl_.size.x;
        float box_height = p_boxColl_.size.y;
        float box_cell_width = box_width / p.GetLength(0);
        float box_cell_height = box_height / p.GetLength(1);
        float x0 = p_boxColl_.transform.position.x - box_width / 2;       // boxコライダー左上の座標を基準にパーティクル生成
        float y0 = p_boxColl_.transform.position.y + box_height / 2;
        float deg = p_boxColl_.transform.localEulerAngles.z;
        for (int r = 0; r < p.GetLength(0); r++)
        {
            for( int c = 0; c < p.GetLength(1); c++)
            {
                // 位置情報決定
                float pos_x = x0 + r * box_cell_width + box_cell_width / 2;
                float pos_y = y0 - c * box_cell_height - box_cell_height / 2;
                // 配置にランダム性を導入する
                pos_x += Random.Range(-box_cell_width / 2 * rand_coff_, box_cell_width / 2 * rand_coff_);
                pos_y += Random.Range(-box_cell_height / 2 * rand_coff_, box_cell_height / 2 * rand_coff_);
                // 構造体に情報を格納していく
                Vector3 delta_pos_to_box = new Vector3(pos_x, pos_y, transform.position.z) - p_boxColl_.transform.position;
                Vector3 delta_pos = (p_boxColl_.transform.position - transform.position) +  Quaternion.Euler(0, 0, deg) * delta_pos_to_box;
                p[r, c].delta_position_ = delta_pos; 
            }
        }

    }


    private bool ModeChange()
    {
        // ---- オーラのモードを変更する ---- //
        if (current_aura_ == pre_aura_) { return false; };    // モード変化が無ければ終了
        
        // --- オーラが円の場合 --- //
        if(current_aura_ == mode_.circle)
        {
            return true;                                      // ここで処理終了
        }
        // --- オーラが四角形の場合 --- //
        Vector3 box_pos_length = transform.position - p_boxColl_.transform.position;
        Vector3 box_dir = new Vector3(0, 0, 0);
        float box_pos_sq = box_pos_length.magnitude;
        float box_deg = 0;

        // -- Boxの角度と方向を決定 -- // 
        if (current_aura_ == mode_.front_box) {
            box_deg = -90;
            box_dir = transform.right;
        }
        if(current_aura_ == mode_.up_box) { 
            box_deg = 0; 
            box_dir = transform.up;
        }
        if(current_aura_ == mode_.back_box)
        {
            box_deg = 90;
            box_dir = -1 * transform.right;
        }
        if(current_aura_ == mode_.down_box)
        {
            box_deg = 0;
            box_dir = -1 * transform.up;
        }
       
        p_boxColl_.transform.rotation = Quaternion.Euler(0, 0, -box_deg);
        p_boxColl_.transform.localPosition = box_dir * box_pos_sq;
        // 1フレーム前のモード格納
        pre_aura_ = current_aura_;                      

        return true;
    }


}
