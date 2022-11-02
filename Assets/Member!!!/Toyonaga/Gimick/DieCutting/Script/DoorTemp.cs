using UnityEngine;
public class DoorTemp : MonoBehaviour
{
    [SerializeField, Tooltip("DieCutting��o�^")]
    DieCutting dc_;
    private Vector3 moved;
    bool is_move_ = true;

    // ---- DieCutting�̎g�p�� ---- //
    void Update()
    {
        // --- �M�~�b�N�������Ă���Ƃ��F���L�֐���True���Ԃ� --- //
        if (dc_.IsComplete())
        {
            // -- ���Ƃ͂���ɉ����Ĕ����̃M�~�b�N�𓮂��� -- //
            if(!is_move_) { return; }
            move();
        }
    }

    void move()
    {
        transform.position += new Vector3(0, 0.1f, 0);
        moved += new Vector3(0, 0.1f, 0);
        if(moved.magnitude > 5) { is_move_ = false; }
    }
}
