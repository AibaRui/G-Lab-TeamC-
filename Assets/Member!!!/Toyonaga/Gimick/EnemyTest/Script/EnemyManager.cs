using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField, Header("MoveArea : �s���͈�")]
    BoxCollider2D move_lim_boxCol_;
    [SerializeField, Header("�ړ����x[1�}�X/s]")]
    private float move_velocity = 1f;
    [SerializeField, Header("RigidBody2D")]
    Rigidbody2D rb2d_;
    [SerializeField, Header("�ǔ���")]
    EnemyWall wall_check_;
    
    private Vector2[] move_limits_ = new Vector2[2];
    private int dir_ = -1;                  // �ړ�����

    enum enemy_mode_
    {
        search, attack, box, melt,
    }
    private enemy_mode_ e_mode = enemy_mode_.search;

    void Start()
    {
        // ----- ������ ----- //
        // ---- �s�����E�͈͐ݒ�(�l�p�`)
        move_limits_[0] = new Vector2(
            move_lim_boxCol_.transform.position.x - move_lim_boxCol_.size.x/2,
            move_lim_boxCol_.transform.position.y + move_lim_boxCol_.size.y/2
            );
        move_limits_[1] = new Vector2(
            move_lim_boxCol_.transform.position.x + move_lim_boxCol_.size.x / 2,
            move_lim_boxCol_.transform.position.y - move_lim_boxCol_.size.y / 2
            );
        // MoveArea�F�ȏ㑀����p�����B����
        move_lim_boxCol_.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 0. ��@���[�h 
        if (enemy_mode_.search == e_mode)
        {
            // ---- �ړ����ǂ��������甽�] ---- //
            GroundEnemyMove(Time.fixedDeltaTime, ref rb2d_, ref wall_check_);
        }



       
    }

    private void GroundEnemyMove(float delta_time, ref Rigidbody2D rb, ref EnemyWall w_check)
    {
        // ---- �n��G�̒ʏ�ړ��F�ړ��͈͓��ōs�� ---- //
        // --- �ړ� --- //
        Vector3 velocity = new Vector3(move_velocity * dir_ * Time.fixedDeltaTime, 0, 0);
        rb.transform.position += velocity;
        if ((rb.transform.position.x < move_limits_[0].x) || (rb.transform.position.x > move_limits_[1].x)) {
            dir_ *= -1;
            transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
        
        // --- �ǔ��� --- //
        }else if (w_check.On_wall) {
            dir_ *= -1;
            transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
        }
    }



}
