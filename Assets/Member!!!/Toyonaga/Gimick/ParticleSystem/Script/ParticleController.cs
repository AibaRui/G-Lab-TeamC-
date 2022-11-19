using UnityEngine;
using System.Collections.Generic;

public class ParticleController : GimickBase
{
    [Header("ポーズ中True"), SerializeField]
    public bool is_Pause_ = false;
    [Header("★円オーラ"), SerializeField]
    private List<CircleCollider2D> p_cir_areas_;
    [Header("★四角オーラ")]
    [SerializeField, Tooltip("コライダーのy軸をそれぞれの方向に回転させたものを登録して下さい")]
    private List<BoxCollider2D> p_box_areas_;
    [Header("★オーラ変更整数: 円：0スタート")]
    [SerializeField, Tooltip("円オーラから0, その後、円オーラのList数、四角オーラのList数に応じて指定する")]
    public int mode_num_ = 0;       // オーラのモードを管理する変数
    [Header("★生成する精霊(パーティクル)を登録"), SerializeField]
    private GameObject p_obj_;
    [Header("★パーティクルの行の数"), SerializeField]
    private int p_row_ = 2;
    [Header("★パーティクルの列の数"), SerializeField]
    private int p_col_ = 5;
    [Header("Setting")]
    [SerializeField, Tooltip("パーティクルのばらつき具合[整数, 1 = 100%]")]
    private float rand_coff_ = 0.5f;
    [SerializeField, Tooltip("パーティクルのばらつき時間間隔[s]")]
    private float rand_time_ = 0.2f;
    private float rand_time_counter = 0;
    [SerializeField, Tooltip("パーティクルを配置しない円の大きさを指定する")]
    private float p_cicle_voidArea_size_ = 0.2f;
    [SerializeField, Header("調整機能：パーティクルパラメータ調整後、「R」を押すと反映されるようになる\n「M」でオーラ変化")]
    public bool is_Tune_ = false;
    private enum mode_              // 円 or 四角の
    {
        circle, box, end
    }
    private mode_ aura_mode_ = mode_.circle;

    private struct particle         // パーティクル情報を一括管理
    {
        public GameObject particle_;
        public ParticleMove pm_;
        public Vector3 position_;
        public Vector3 delta_position_;
    }
    private particle[,] p_st_;

    private void Start()
    {
        // ---- 初期化処理 ---- //
        p_st_ = GenerateParticles(ref aura_mode_);
    }

    private void Update()
    {
        // ---- 調整機能 ---- //
        if (!is_Tune_) { return; }
        // --- オーラの状態を変えたい時に使用 --- //
        if (Input.GetKeyDown(KeyCode.M))
        {
            mode_num_++;
            ChangeAura(ref aura_mode_, ref mode_num_);     // インスペクタ：mode_num_の変更に応じてオーラの状態変化
        }
        // --- パーティクルのパラメータ調整後、反映させたい時に使用 --- //
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int r = 0; r < p_st_.GetLength(0); r++)
            {
                for (int c = 0; c < p_st_.GetLength(1); c++)
                {
                    Destroy(p_st_[r, c].particle_.gameObject);
                }
            }
            p_st_ = GenerateParticles(ref aura_mode_);
        }
        

    }

    private void FixedUpdate()
    {
       
        // ---- ポーズ中は動作無し ---- //
        if (is_Pause_) { return; }
        // ---- 実行中の処理 ---- //
        ChangeAura(ref aura_mode_, ref mode_num_);     // インスペクタ：mode_num_の変更に応じてオーラの状態変化
        if (aura_mode_ == mode_.circle) { ParticleRandomUpdate_Circle(ref p_st_, ref mode_num_, Time.deltaTime); }
        if (aura_mode_ == mode_.box) { ParticleRandomUpdate_Box(ref p_st_, ref mode_num_, Time.deltaTime); }
        // ---- パーティクル位置のアップデート：親(ParticleSystem)座標からの偏差位置：p_st.delta_positionを使用して更新 --- //
        for (int r = 0; r < p_row_; r++)
        {
            for (int c = 0; c < p_col_; c++)
            {
                p_st_[r, c].pm_.TargetPos = transform.position + p_st_[r, c].delta_position_;
            }
        }
    }

    private particle[,] GenerateParticles(ref mode_ m)
    {
        // ---- パーティクルをゲームオブジェクト範囲内に生成 ---- //
        particle[,] p = new particle[p_row_, p_col_];
        mode_ temp_mode = m;                    // 現在のモードを保存しておく
        m = mode_.box;                          // ボックスモードで生成実施
        if (p_box_areas_[0] == null) { Debug.Log("Error, Please Set to Box Collider"); }
        BoxCollider2D box = p_box_areas_[0];    // 登録したボックスコライダーを一時取得
        // ---- p_stの情報を作成していく ---- //
        Vector3 box_d_pos = transform.position - box.transform.position;    // 親からボックスまでの距離取得
        float box_width = box.size.x;
        float box_height = box.size.y;
        float box_cell_width = box.size.x / p_row_;
        float box_cell_height = box.size.y / p_col_;
        float x0 = box.transform.position.x - box_width / 2;        // y軸方向を正とし、boxコライダ左上の座標基準にパーティクル生成
        float y0 = box.transform.position.y - box_height / 2;
        for (int r = 0; r < p_row_; r++)
        {
            for (int c = 0; c < p_col_; c++)
            {
                // --- 位置情報決定 --- //
                float pos_x = x0 + r * box_cell_width + box_cell_width / 2;
                float pos_y = y0 - c * box_cell_height - box_cell_height / 2;
                // 配置にランダム性を導入する
                pos_x += Random.Range(-box_cell_width / 2 * rand_coff_, box_cell_width / 2 * rand_coff_);
                pos_y += Random.Range(-box_cell_height / 2 * rand_coff_, box_cell_height / 2 * rand_coff_);
                // --- ゲームオブジェクトのインスタンス＆情報指定 --- //
                var obj = Instantiate(p_obj_);
                obj.transform.parent = transform;   // 親を本ゲームオブジェクトに設定
                obj.transform.position = new Vector3(pos_x, pos_y, transform.position.z);   // boxコライダー指定の位置に移動
                // --- 構造体に情報を格納していく --- //
                p[r, c].particle_ = obj;
                p[r, c].pm_ = obj.GetComponent<ParticleMove>();
                p[r, c].position_ = new Vector3(pos_x, pos_y, transform.position.z);
                p[r, c].delta_position_ = p[r, c].position_ - transform.position;
                p[r, c].pm_.TargetPos = p[r, c].position_;
            }
        }
        m = temp_mode;          // モードを戻す

        return p;
    }

    private void ParticleRandomUpdate_Box(ref particle[,] p, ref int mode_num, float delta_time)
    {
        // ---- パーティクルをランダムに動かすため、制御目標位置の基となる偏差位置情報を作成＆格納: BoxCollider2D対象 ---- //
        // --- ランダムタイムカウント --- //
        rand_time_counter += delta_time;
        if (rand_time_counter < rand_time_) { return; }
        rand_time_counter = 0;
        // --- パーティクルの目標位置更新 --- //
        int box_num = mode_num - p_cir_areas_.Count;
        BoxCollider2D box = p_box_areas_[box_num];      // パーティクル更新対象を一時参照
        float box_width = box.size.x;
        float box_height = box.size.y;
        float box_cell_width = box_width / p.GetLength(0);
        float box_cell_height = box_height / p.GetLength(1);
        float local_x0 = -box_width / 2;
        float local_y0 = box_height / 2;
        for (int r = 0; r < p.GetLength(0); r++)
        {
            for (int c = 0; c < p.GetLength(1); c++)
            {
                // ボックスローカル座標でパーティクルの目標位置計算
                Vector3 local_pos = new Vector3(
                    local_x0 + r * box_cell_width + box_cell_width / 2,
                    local_y0 - c * box_cell_height - box_cell_height / 2,
                    transform.position.z
                    );
                // ボックスローカル座標：配置にランダム性を導入する
                local_pos += new Vector3(
                    Random.Range(-box_cell_width / 2 * rand_coff_, box_cell_width / 2 * rand_coff_),
                    Random.Range(-box_cell_height / 2 * rand_coff_, box_cell_height / 2 * rand_coff_),
                    0
                    );
                // 構造体に情報を格納していく：親からパーティクルの目標位置の偏差情報を取得しておけば、座標更新が出来る
                float deg = box.transform.eulerAngles.z;    // ボックス回転量(deg)取得
                Vector3 delta_pos = (box.transform.position - transform.position) + Quaternion.Euler(0, 0, deg) * local_pos;
                p[r, c].delta_position_ = delta_pos;
            }
        }
    }

    private void ParticleRandomUpdate_Circle(ref particle[,] p, ref int mode_num, float delta_time)
    {
        // ---- パーティクルをランダムに動かすため、制御目標位置の基となる偏差位置情報を作成＆格納：CircleCollider2D対象 ---- //
        // --- ランダムタイムカウント --- //
        rand_time_counter += delta_time;
        if (rand_time_counter < rand_time_) { return; }
        rand_time_counter = 0;
        // --- パーティクルの目標位置更新 --- //
        Vector3 particle_delta_pos_ = new Vector3(0, 0, 0);       // 一時格納変数
        int i = 0;
        float deg = 0;                                                  // パーティクルを配置する角度
        float d_deg = 360 / p.GetLength(0) / p.GetLength(1);   // パーティクルを配置する刻み角度
        for (int r = 0; r < p.GetLength(0); r++)
        {
            for (int c = 0; c < p.GetLength(1); c++)
            {
                // -- パーティクル円形内かつ、p_circle_void_Area_sizeより大きい位置にパーティクルをランダムに配置していく -- //
                float cir_span = p_cir_areas_[mode_num].radius - p_cicle_voidArea_size_ / 2;
                particle_delta_pos_ = transform.right * (cir_span / 2 + p_cicle_voidArea_size_ / 2);
                // - 配置にランダム性を持たせる - //
                particle_delta_pos_ += transform.right * Random.Range(-cir_span * rand_coff_, cir_span * rand_coff_);
                // - 回転 - //
                particle_delta_pos_ = Quaternion.Euler(0, 0, deg) * particle_delta_pos_;

                p[r, c].delta_position_ = particle_delta_pos_;
                deg += d_deg;
                i++;    // 1次元配列の参照先更新
            }
        }
    }

    private void ChangeAura(ref mode_ m, ref int mode_num)
    {
        // ---- モードチェンジ ---- //
        int cir_num = p_cir_areas_.Count;
        int box_num = p_box_areas_.Count;
        if (cir_num + box_num <= mode_num)
        {
            mode_num = 0;
            m = mode_.circle;
        }
        if (mode_num < cir_num) { m = mode_.circle; }
        if (mode_num >= cir_num) { m = mode_.box; }
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