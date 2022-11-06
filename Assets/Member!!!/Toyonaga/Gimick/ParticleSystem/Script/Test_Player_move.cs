using UnityEngine;

public class Test_Player_move : MonoBehaviour
{

    // ---- PartcicleSystem�̎g�p�� ---- //
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

        // --- ParticleSystem:�ȉ��̂Q�p�^�[���Ŏg�p --- //
        if (Input.GetKeyDown(KeyCode.Space)){
            // -- �w��̃I�[���̃��[�h�i�~ or �l�p(�O(transform.right���� , ��(tranform.up����, ��A ��))�ŏ�ԕω� -- //
            Pc.SetAuraChange(ParticleController.mode_.circle);      
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // -- ��x�Ăяo���x�A�I�[���̃��[�h���~�A�l�p(�O�A��A��A���j�A�~..�̏��ŕύX -- //
            Pc.SetAuraChangeRow();
        }

    }
}
