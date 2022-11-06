using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2D_;      // ���W�b�h�{�f�B�擾
    // ---- ����p�����[�^ ---- //
    [Header("���x���"), SerializeField]
    float vel_max_ = 5f;                 // ���x����
    [Header("���x�����_����� < vel_max"), SerializeField]
    float vel_rand_max_ = 3.5f;
    [Header("���x�����_���̓x��(0-1:velmax"), SerializeField]
    float vel_rand_coff_ = 0.5f;
    [SerializeField, Tooltip("�����قǒ����G�ɖڕW�ʒu�ֈړ�,���ʐݒ���d�v")]
    private float kp_ = 5f;              // P����W��
    [SerializeField, Tooltip("�����قǐ����ɖڕW�ʒu�ֈړ��A���ʐݒ���d�v")]
    private float ki_ = 0.01f;
    [SerializeField, Tooltip("�����قǏr�q�ɖڕW�ʒu�ֈړ�, ���ʐݒ���d�v")]
    private float kd_ = 3f;
    private Vector3 pre_error_;         // 1�t���[���O�̌덷�i�[
    private Vector3 integral_error_;    // �덷�ݐϒl
    private Vector3 target_pos_;        // �p�[�e�B�N���ړ��ڕW�ʒu
    public Vector3 TargetPos { get { return target_pos_; } set { target_pos_ = value; } }

    private void Start()
    {
        // ---- ���������� ---- //
        rb2D_ = GetComponent<Rigidbody2D>();
        vel_max_ += Random.Range(-vel_rand_max_ * vel_rand_coff_, vel_rand_max_ * vel_rand_coff_);
    }

    private void FixedUpdate()
    {
        // ---- �p�[�e�B�N����ڕW�ʒu(target_pos_)�֐��� ---- //
        rb2D_.AddForce(MoveCont(ref target_pos_, Time.deltaTime));
        if(rb2D_.velocity.sqrMagnitude > vel_max_ * vel_max_)   // ���[�g�v�Z�͏d���̂ŁA���莮�ł͔�����
        {
            rb2D_.velocity = rb2D_.velocity.normalized * vel_max_;  // ���x����
        }
    }

    private Vector3 MoveCont(ref Vector3 target, float delta_time)
    {
        // ---- �p�[�e�B�N�����ڕW�ʒu�֌������߂̗̓x�N�g����Ԃ� ---- //
        Vector3 pow = new Vector3 (0, 0, 0);
        Vector3 posError = target - transform.position;     // �덷�擾
        // --- ������͐��� --- //
        integral_error_ = (posError + pre_error_) / 2 * delta_time;
        pow =   kp_ * posError +
                ki_ * integral_error_ +
                kd_ * (posError - pre_error_) / delta_time;
        pre_error_ = posError;      // 1 delta_time_�O�̈ʒu�덷���i�[���Ă���
        
        return pow;
    }

}
