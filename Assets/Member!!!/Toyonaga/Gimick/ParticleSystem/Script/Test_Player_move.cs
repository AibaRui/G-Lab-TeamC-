using UnityEngine;

public class Test_Player_move : MonoBehaviour
{

    // ---- PartcicleSystemの使用例 ---- //
    public Rigidbody2D rb2D;
    public ParticleController Pc; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rb2D.AddForce(new Vector2(h * 5, v * 5));

        // --- ParticleSystem:以下の２パターンで使用 --- //
        if (Input.GetKeyDown(KeyCode.Space)){
            // -- 指定のオーラのモード（円 or 四角(前(transform.right方向 , 上(tranform.up方向, 後、 下))で状態変化 -- //
            Pc.SetAuraChange(ParticleController.mode_.circle);      
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // -- 一度呼び出す度、オーラのモードを円、四角(前、上、後、下）、円..の順で変更 -- //
            Pc.SetAuraChangeRow();
        }

    }
}
