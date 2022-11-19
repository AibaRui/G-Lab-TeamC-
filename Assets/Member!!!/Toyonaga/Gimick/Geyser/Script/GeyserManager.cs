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
