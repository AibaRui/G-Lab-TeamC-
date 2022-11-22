using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TurretController : GimickBase
{
    [SerializeField, Tooltip("射出するゲームオブジェクト")]
    private GameObject[] _shell = default;
    [SerializeField, Range(15f, 75f), Tooltip("射出する角度")]
    private float _angle = 45f;
    [SerializeField, Tooltip("砲台の射程距離")]
    private float _range = 1f;
    [SerializeField, Tooltip("射出する間隔")]
    private float _interval = 1f;
    /// <summary>投擲する間隔をカウントする</summary>
    private float _timer = 0f;
    [SerializeField, Tooltip("攻撃対象")]
    private Transform[] _terget;
    /// <summary>現在、Pause中かどうかを判定するフラグ</summary>
    private bool _isPause = true;

    private void Update()
    {
        if(_isPause)
        {
            //一定間隔ごとに弾を射出する
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                ThrowingShell();
                _timer = 0;
            }
        }
    }

    /// <summary>弾を射出するメソッド</summary>
    private void ThrowingShell()
    {
        if (_shell.Length > 0 && _terget.Length > 0)
        {
            //攻撃対象の中からランダムで選ばれたターゲットを狙う
            int number = Random.Range(0, _terget.Length);

            //弾の中からランダムで選ばれたものを射出する
            int random = Random.Range(0, _shell.Length);
            
            //射出速度を計算する
            Vector2 velocity = CalculateVelocity(transform.position, _terget[number].position, _angle);

            if(velocity != Vector2.zero)
            {
                //弾を生成する
                GameObject shell = Instantiate(_shell[random], transform.position, Quaternion.identity);

                //弾にRigidbody2Dがアタッチされていることを確約する
                Rigidbody2D shellRb;
                if (shell.TryGetComponent(out Rigidbody2D rb)) shellRb = rb;
                else shellRb = shell.AddComponent<Rigidbody2D>();

                //Playerのいる座標に弾を射出する
                shellRb.AddForce(velocity * shellRb.mass, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("投擲するオブジェクト or 攻撃対象 がassignされていません。");
        }
    }

    /// <summary>射出するときの初速度を求めるメソッド</summary>
    /// <param name="muzzle">砲口</param>
    /// <param name="terget">ターゲット地点</param>
    /// <param name="angle">射出角度</param>
    /// <returns>計算により求められた初速度</returns>
    private Vector2 CalculateVelocity(Vector2 muzzle, Vector2 terget, float angle)
    {
        //弧度法を度数法に変換
        float rad = angle * Mathf.PI / 180;

        //水平方向の距離を求める
        float x = Vector2.Distance(new Vector2(muzzle.x, 0), new Vector2(terget.x, 0));

        //鉛直方向の距離を求める
        float y = muzzle.y - terget.y;

        //斜方投射の式を初速度について解く
        float speed = Mathf.Sqrt(-Physics2D.gravity.y * x * x / 
            (2 * Mathf.Cos(rad) * Mathf.Cos(rad) * (x * Mathf.Tan(rad) + y)));

        //speedの計算結果が実数値でない時 or 射程範囲外の時は、初速度を０にする。
        if (float.IsNaN(speed) || x > _range)
        {
            return Vector2.zero;
        }
        else
        {
            return (new Vector2(terget.x - muzzle.x, x * Mathf.Tan(rad)).normalized * speed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    public override void GameOverPause()
    {
        _isPause = false;
    }

    public override void Pause()
    {
        _isPause = false;
    }

    public override void Resume()
    {
        _isPause = true;
    }
}
