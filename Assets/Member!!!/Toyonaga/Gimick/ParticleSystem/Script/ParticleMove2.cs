using UnityEngine;

public class ParticleMove2 : GimickBase
{
    [SerializeField]
    private bool is_Pause_ = false;
    [SerializeField]
    private Rigidbody2D rb2D_;      // リジッドボディ取得
    // ---- 制御パラメータ ---- //
    [Header("速度上限"), SerializeField]
    float vel_max_ = 5f;                 // 速度制限
    [Header("速度ランダム上限 < vel_max"), SerializeField]
    float vel_rand_max_ = 3.5f;
    [Header("速度ランダムの度合(0-1:velmax"), SerializeField]
    float vel_rand_coff_ = 0.5f;
    [SerializeField, Tooltip("高いほど直線敵に目標位置へ移動,質量設定も重要")]
    private float kp_ = 5f;              // P制御係数
    [SerializeField, Tooltip("高いほど精密に目標位置へ移動、質量設定も重要")]
    private float ki_ = 0.01f;
    [SerializeField, Tooltip("高いほど俊敏に目標位置へ移動, 質量設定も重要")]
    private float kd_ = 3f;
    private Vector3 pre_error_;         // 1フレーム前の誤差格納
    private Vector3 integral_error_;    // 誤差累積値
    private Vector3 target_pos_;        // パーティクル移動目標位置
    public Vector3 TargetPos { get { return target_pos_; } set { target_pos_ = value; } }
    

    private void Start()
    {
        // ---- 初期化処理 ---- //
        rb2D_ = GetComponent<Rigidbody2D>();
        vel_max_ += Random.Range(-vel_rand_max_ * vel_rand_coff_, vel_rand_max_ * vel_rand_coff_);
       
    }

    private void FixedUpdate()
    {
        // ----- ポーズ中は動きを停止 ----- //
        if (is_Pause_)
        {
            rb2D_.constraints = RigidbodyConstraints2D.FreezeAll;

            return;
        }
        else
        {
            // 解除
            rb2D_.constraints = RigidbodyConstraints2D.None;
        }

        // ---- パーティクルを目標位置(target_pos_)へ制御 ---- //
        rb2D_.AddForce(MoveCont(ref target_pos_, Time.deltaTime));
        if (rb2D_.velocity.sqrMagnitude > vel_max_ * vel_max_)   // ルート計算は重いので、判定式では避ける
        {
            rb2D_.velocity = rb2D_.velocity.normalized * vel_max_;  // 速度制限
        }
    }

    private Vector3 MoveCont(ref Vector3 target, float delta_time)
    {
        // ---- パーティクルが目標位置へ向うための力ベクトルを返す ---- //
        Vector3 pow = new Vector3(0, 0, 0);
        Vector3 posError = target - transform.position;     // 誤差取得
        // --- 制御入力生成 --- //
        integral_error_ = (posError + pre_error_) / 2 * delta_time;
        pow = kp_ * posError +
                ki_ * integral_error_ +
                kd_ * (posError - pre_error_) / delta_time;
        pre_error_ = posError;      // 1 delta_time_前の位置誤差を格納しておく

        return pow;
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