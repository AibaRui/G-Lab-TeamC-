using UnityEngine;

public class GeyserManager : GimickBase
{
    [SerializeField, Header("���I�[���̖��O��o�^")]
    private string fire_aura_;
    public string Fire_aura { get { return fire_aura_; } }
    [SerializeField, Header("�X�I�[���̖��O��o�^")]
    private string ice_aura_;
    public string Ice_aura { get { return ice_aura_; } }
    [SerializeField, Header("�|�[�Y���AGeyser�̓�����S�Ď~�߂�")]
    public bool is_Pause_ = false;
    [SerializeField, Header("�X�̊₪�n�������ǂ���")]
    public bool is_IceRock_melted_ = false;
    [SerializeField, Header("�v���C���[1�^�O��")]
    private string player1_name;
    [SerializeField, Header("�v���C���[2�^�O��")]
    private string player2_name;
    [SerializeField, Header("�I�[�f�B�I�\�[�X1")]
    private AudioSource ad1;
    [SerializeField, Header("�I�[�f�B�I�\�[�X2")]
    private AudioSource ad2;
    [SerializeField]
    private bool is_SE_active = false;
    [SerializeField, Header("�X���n���鉹")]
    private AudioClip iceMelt_sound;
    [SerializeField, Header("�X���ł܂鉹")]
    private AudioClip iceMake_sound;
    [SerializeField, Header("�Ԍ��򂪗N����")]
    private AudioClip water_gush_sound;
    [SerializeField, Header("�Ԍ��򂪐����o����")]
    private AudioClip water_spewOut_sound;

    public enum  se_fountain{
        normal, 
        spewing,
    }
    private se_fountain se_fo = se_fountain.normal;
    private se_fountain se_fo_pre = se_fountain.normal;  // 1�t���[���O�̍Đ�SE�i�[

    public enum se_ice
    {
        none,
        ice_making,
        ice_melting,
    }
    private se_ice se_ic = se_ice.none;
    private se_ice se_ic_pre = se_ice.none;     // 1�t���[���O�̍Đ�SE�i�[ 


    private void Update()
    {
        // ---- SE�Ǘ� ---- //
        if (!is_SE_active) { return; }  // �v���C���[���͈͓��ɋ��Ȃ��FSE����
        // 1. ����SE�Ǘ�
        switch (se_fo)
        {
            case se_fountain.normal:
                //if (se_fo != se_fo_pre) { ad1.Stop(); }   // �N���������͏�ɍĐ�
                if (ad1.isPlaying) { break; }
                ad1.PlayOneShot(water_gush_sound);
                break;

            case se_fountain.spewing:
                if (se_fo != se_fo_pre) { ad1.Stop(); }     // 1�t���[���O�̐ݒ�SE�ƈႤ�F��~
                if (ad1.isPlaying) { break; }
                ad1.PlayOneShot(water_spewOut_sound);
                break;
        }
        se_fo_pre = se_fo;                                  // 1�t���[���O�̐ݒ�SE���i�[

        // 2. �XSE�Ǘ�
        switch (se_ic)
        {
            case se_ice.none:
                
                break;

            case se_ice.ice_making:
                if (se_ic != se_ic_pre) { break; }
                if (ad2.isPlaying) { break; }
                ad2.PlayOneShot(iceMake_sound);
                se_ic = se_ice.none;
                break;

            case se_ice.ice_melting:
                if (se_ic != se_ic_pre) { break; }
                if (ad2.isPlaying) { break; }
                ad2.PlayOneShot(iceMelt_sound);
                se_ic = se_ice.none;
                break;
        }
        se_ic_pre = se_ic;                                  // 1�t���[���O�̐ݒ�SE���i�[


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == player1_name || collision.gameObject.tag == player2_name)
        {
            is_SE_active = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player1_name || collision.gameObject.tag == player2_name)
        {
            is_SE_active = false;
        }
    }

    public void SetFountainSE(se_fountain se_f)
    {
        // ---- ������SE�̃^�C�v��ύX ---- //
        se_fo = se_f;
    }

    public void SetIceSE(se_ice se_i)
    {
        // ---- �X��SE�̃^�C�v�ύX ---- //
        se_ic = se_i;
    }

    public override void GameOverPause()
    {
        is_Pause_ = true;
    }

    public override void Pause()
    {
        is_Pause_ = true;
    }

    public override void Resume()
    {
        is_Pause_ = false;
    }

}
