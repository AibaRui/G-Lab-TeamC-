using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("���G�Ώۂ̃^�O���FSearchArea���ɂ���Δ������U������")]
    private List<string> target_tag_;
    public List<string> Target_tag_ { get { return target_tag_; } }
    [SerializeField, Header("MoveArea : �s���͈�")]
    private BoxCollider2D move_lim_boxCol_;
    [SerializeField, Header("�ړ����x[1�}�X/s]")]
    private float move_velocity = 1f;
    [SerializeField, Header("RigidBody2D")]
    private Rigidbody2D rb2d_;
    [SerializeField, Header("�ǔ���")]
    private EnemyWall Wall_check_;
    [SerializeField, Header("���G����")]
    private EnemySearch Target_seartch_;

    
    private Vector2[] move_limits_ = new Vector2[2];
    private int dir_ = -1;                  // �ړ�����
    private Transform target_trans_;        // �U���Ώۂ�Transform���擾


    enum enemy_mode_
    {
        search, attack, box, melt,
    }
    private enemy_mode_ e_mode = enemy_mode_.search;

    void Start()
    {
        // ----- ������ ----- //
        // ---- �s�����E�͈͐ݒ�(�l�p�`)
        move_limits_ = moveLimits(ref move_lim_boxCol_);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 0. ��@���[�h 
        if (enemy_mode_.search == e_mode)
        {
            // ---- ���G���[�h������ ----- //
            if (Target_seartch_.Is_target_ == true) {
                Target_seartch_.Is_target_ = false;     // ���G�N���X����J�n
                target_trans_ = null;                   // ���G�Ώۂ���Unull
            }
            // ---- �ړ����ǂ��������甽�] ---- //
            GroundEnemyMove(Time.fixedDeltaTime, ref rb2d_, ref Wall_check_);
            // ---- �^�[�Q�b�g�𔭌��������F�^�[�Q�b�g��Transform�擾�����[�h�ω�( to 1) ---- //
            if(Target_seartch_.Target_trans_ != null)
            {
                target_trans_ = Target_seartch_.Target_trans_;
                e_mode = enemy_mode_.attack;
            }

        // 1. �U�����[�h
        } else if(enemy_mode_.attack == e_mode)
        {
           // ---- �^�[�Q�b�g�֍U�����邽�ߋʐ������������� ---- //

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

    private Vector2[] moveLimits(ref BoxCollider2D limBox)
    {
        Vector2[] limit_rl = new Vector2[2];
        limit_rl[0] = new Vector2(
            move_lim_boxCol_.transform.position.x - move_lim_boxCol_.size.x / 2,
            move_lim_boxCol_.transform.position.y + move_lim_boxCol_.size.y / 2
            );
        limit_rl[1] = new Vector2(
            move_lim_boxCol_.transform.position.x + move_lim_boxCol_.size.x / 2,
            move_lim_boxCol_.transform.position.y - move_lim_boxCol_.size.y / 2
            );
        // MoveArea�F�ȏ㑀����p�����B����
        limBox.gameObject.SetActive(false);
        return limit_rl;
    }



}
