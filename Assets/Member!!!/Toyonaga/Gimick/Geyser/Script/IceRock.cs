using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRock : MonoBehaviour
{
    [SerializeField, Header("GeyserManager�o�^")]
    GeyserManager manager_;
    [SerializeField, Header("IceRock��RigidBody2D�o�^")]
    Rigidbody2D rb_;
    [SerializeField, Header("IceRock�̉摜�����Q�[���I�u�W�F�N�g�o�^")]
    GameObject iceRockObj_;
    [SerializeField, Header("�����݂ɗh���Ԋu����")]
    private float small_vibration_time_ = 1f;
    [SerializeField, Header("�����݂ɗh���p�x(deg)")]
    private float small_vibration_mag_ = 20;
    [SerializeField, Header("IceRock�̃T�C�Y���ς�闦�w��")]
    private float transform_rate_ = 1.0f;

    private float init_scaleX = 1f;     // ������Ԃ�IceRockGameObject�̃T�C�Y�o�^
    private float init_scaleY = 1f;

    private void Start()
    {
        // ----- iceRock�̏����T�C�Y�i�[ ----- //
        init_scaleX = iceRockObj_.transform.localScale.x;
        init_scaleY = iceRockObj_.transform.localScale.y;
    }

    private void FixedUpdate()
    {
        // ----- �|�[�Y���͓������~ ----- //
        if (manager_.is_Pause_){ 
            rb_.constraints = RigidbodyConstraints2D.FreezeAll;

            return;
        } else {
            // ����
            rb_.constraints = RigidbodyConstraints2D.None;
            rb_.constraints = RigidbodyConstraints2D.FreezePositionX;   // x���͍S��
        }
        // ----- �ʏ퓮�� ----- //
        // --- ������ --- //
        float rad = small_vibration_mag_ * Mathf.Sin(Time.time * (1/(small_vibration_time_/2)));
        smallVib(ref rad, Time.deltaTime);      // �u���u���k���鏈��

    }

    private void smallVib(ref float rad, float delta_time)
    {
        iceRockObj_.transform.localEulerAngles = new Vector3(0, 0, rad);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ----- �I�[�����󂯂����̏��� ----- //
        // ---- �� ---- //
        if (other.transform.CompareTag(manager_.Fire_aura))
        {
            transform.localScale -= new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            iceRockObj_.transform.localScale -= new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            //  --- SE�Ǘ� --- //
            manager_.SetIceSE(GeyserManager.se_ice.ice_melting);

        } else if (other.transform.CompareTag(manager_.Ice_aura))
        // ---- �X ---- //
        {
            if(init_scaleX < transform.localScale.x) { return; }    // �傫������
            transform.localScale += new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            iceRockObj_.transform.localScale += new Vector3(
                Time.deltaTime * transform_rate_,
                Time.deltaTime * transform_rate_,
                0);
            // --- SE�Ǘ� --- //
            manager_.SetIceSE(GeyserManager.se_ice.ice_making);
        }
        // ----- ���ȏ�̃T�C�Y�ɏ������Ȃ������F�{�Q�[���I�u�W�F�N�g������ ---- //
        // ---- �}�l�[�W���[��IceRock���폜���ꂽ����`���� ---- //
        // ---- �摜�Q�[���I�u�W�F�N�g�������� ---- //
        if(transform.localScale.x < init_scaleX / 5)
        {
            manager_.is_IceRock_melted_ = true;
            this.gameObject.SetActive(false);
            iceRockObj_.SetActive(false);
        }

    }


}
