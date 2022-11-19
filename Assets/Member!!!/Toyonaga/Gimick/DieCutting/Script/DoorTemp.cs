using UnityEngine;
public class DoorTemp : MonoBehaviour
{
    // ---- DieCutting�N���X�̎g�p���@ ---- //
    [SerializeField, Tooltip("DieCuttingManager��o�^")]
    DieCuttingManager dc_;

    // - (�{�M�~�b�N�̕ϐ�) -
    private Vector3 moved;
    bool is_move_ = true;

    // ---- DieCutting�̎g�p�� ---- //
    void Update()
    {
        // --- �M�~�b�N�������Ă���Ƃ��F���L�֐���True���Ԃ� --- //
        if (dc_.is_complete_)
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
