using UnityEngine;

public class Test_Player_move : MonoBehaviour
{

    // ---- PartcicleSystem�̎g�p�� ---- //
    public Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rb2D.AddForce(new Vector2(h * 5, v * 5));

        // --- ParticleSystem:�ȉ��̂Q�p�^�[���Ŏg�p --- //
        if (Input.GetKeyDown(KeyCode.Space)){
            
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
         
        }

    }
}
