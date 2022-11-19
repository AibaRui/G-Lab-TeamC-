using UnityEditor;
using UnityEngine;

public class DieCuttingManager : GimickBase
{
    [SerializeField, Header("�|�[�Y���͖{�M�~�b�N����")]
    public bool is_Pause_ = false;
    [SerializeField, Header("�����t���O�F�M�~�b�N��������True�ƂȂ�")]
    public bool is_complete_ = false;
    [SerializeField, Header("����{�̐�摜��o�^")]
    private Sprite snow_ref_img_;
    [SerializeField, Header("����{�̐��摜��o�^")]
    private Sprite water_ref_img_;
    [SerializeField, Header("�M�~�b�N��摜��o�^")]
    private Sprite snow_img_;
    [SerializeField, Header("�M�~�b�N���摜��o�^")]
    private Sprite water_img_;
    [SerializeField, Header("���I�[���������")]
    private string fire_tag_name_;
    [SerializeField, Header("�X�I�[���������")]
    private string ice_tag_name_;
    [SerializeField, Header("����{�u���b�N�Ԋu���w��")]
    private float ref_box_span_ = 0f;
    [SerializeField, Header("�M�~�b�N�u���b�N�Ԋu���w��")]
    private float gim_box_span_ = 0f;
    [SerializeField, Header("�u���b�N�̏�ԕω����Ԃ�ݒ�")]
    private float box_change_time_ = 1f;
    [SerializeField, Header("����{�u���b�N�\���ʒu�Q�[���I�u�W�F�N�g�o�^")]
    private GameObject ref_pos_;
    [SerializeField, Header("����{�u���b�N�̃Q�[���I�u�W�F�N�g�o�^")]
    private GameObject ref_box_;
    [SerializeField, Header("�M�~�b�N�u���b�N�\���ʒu�Q�[���I�u�W�F�N�g�o�^")]
    private GameObject gim_pos_;
    [SerializeField, Header("�M�~�b�N�u���b�N�̃Q�[���I�u�W�F�N�g�o�^")]
    private GameObject gim_box_;

    private struct Gim_boxes_
    {
        // --- �M�~�b�N�{�b�N�X�F�\���̂Ŋy�ɊǗ������� --- //
        public GameObject gim_box_;           // �M�~�b�N�u���b�N�Q�[���I�u�W�F�N�g�Q�Ɛ���i�[
        public DieCuttingGimBox gim_script_;    // �M�~�b�N�u���b�N�X�N���v�g�̎Q�Ɛ�i�[
    }
    Gim_boxes_[,] gim_boxes_st_;

    // ------------------------------------------------//
    // ---- �v�ҏW�F��摜�F0, ���摜 : 1�ŏ����� ---- //
    // --- �Q�ƃu���b�N�̏�����Ԃ��`���Ă��� --- //
    private int[,] ref_state_ = new int[4, 4]
    {
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {1, 0, 0, 0, },
        {1, 0, 0, 0, },
    };                    
    // --- �M�~�b�N�u���b�N�̏�����Ԃ��`���Ă��� --- //
    // ���F�K��ref_state_�Ɠ����T�C�Y�Œ�`���鎖�B
    private int[,] gim_state_ = new int[4, 4]
    {
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
    };
    // ------------------------------------------------//

    private void Start()
    {
        // ----- ����{�u���b�N, �M�~�b�N�u���b�N���� ----- //
        GenerateRefBoxes(ref ref_state_, ref ref_pos_, ref ref_box_, ref snow_ref_img_, ref water_ref_img_, ref_box_span_);
        GenerateGimBoxes(ref gim_state_, ref gim_boxes_st_, ref gim_pos_, ref gim_box_, ref snow_img_, ref water_img_, gim_box_span_);
    }

    private void FixedUpdate()
    {
        // ----- �M�~�b�N�u���b�N�ƎQ�ƃu���b�N�̏�Ԕ�r: ���v�����is_complete = true ----- //
        if (is_Pause_) { return; }           // �|�[�Y���͓��얳��
        if (is_complete_) { return; }       // ��x�ł��M�~�b�N�����F�M�~�b�N����s�v
        // ---- ��Ԕ�r ---- //
        bool stateChanged = false;
        for (var r = 0; r < gim_boxes_st_.GetLength(0); r++)
        {
            for(var c = 0; c < gim_boxes_st_.GetLength(1); c++)
            {
                if (gim_boxes_st_[r, c].gim_script_.Is_state_changed)
                {
                    stateChanged = true;
                    break;
                }
                if (stateChanged) { break; }
            }
        }
        // ---- �{�b�N�X�̏�Ԃ�1�u���b�N�ł��ω����������ꍇ�F��Ԕ�r ---- //
        if (!stateChanged) { return; }
        bool isMatched = true;
        for(var r = 0; r < gim_boxes_st_.GetLength(0); r++)
        {
            for(var c = 0; c < gim_boxes_st_.GetLength(1); c++)
            {
                if(ref_state_[r,c] != gim_boxes_st_[r, c].gim_script_.State) { isMatched = false; break; }
            }
            if(!isMatched) { break; }
        }
        if (isMatched) { is_complete_ = true; }
    }


    private void GenerateRefBoxes(ref int[,] ref_a, ref GameObject pos_obj, ref GameObject gen_box, ref Sprite snow, ref Sprite water, float span)
    {
        // ---- �Q�ƃu���b�N�����Ԋu�Ő������Ă��� ---- //
        // --- ������ --- //
        DestroyChildren(pos_obj);   // �����ʒu�Q�ƃQ�[���I�u�W�F�N�g�̎q����U�폜
        for(var r = 0; r < ref_a.GetLength(0); r++)
        {
            for(var c = 0; c < ref_a.GetLength(1); c++)
            {
                // -- �u���b�N���� -- //
                var box = Instantiate(gen_box);
                box.name = $"RefBox({r}, {c})";
                box.transform.parent = pos_obj.transform;
                var spriteR = box.GetComponent<SpriteRenderer>();
                if (ref_a[r, c] == 0) { spriteR.sprite = snow; }
                if(ref_a[r, c] == 1) { spriteR.sprite = water; }
                box.transform.position = new Vector3(
                    pos_obj.transform.position.x + c * (spriteR.size.x + span),
                    pos_obj.transform.position.y - r * (spriteR.size.y + span),
                    pos_obj.transform.position.z
                    );
                // �� �Q�ƃu���b�N�͕`�悷�邾���Ȃ̂ŎQ�Ɠ��͊i�[����

            }
        }
    }
   
    private void GenerateGimBoxes(ref int[,] ref_a, ref Gim_boxes_[,] g_boxes, ref GameObject pos_obj, ref GameObject gen_box, ref Sprite snow, ref Sprite water, float span)
    {
        // ---- �M�~�b�N�u���b�N�����Ԋu�Ő������Ă��� ---- //
        // --- ������ --- //
        DestroyChildren(pos_obj);   // �����ʒu�Q�ƃQ�[���I�u�W�F�N�g�̎q����U�폜
        g_boxes = new Gim_boxes_[ref_a.GetLength(0), ref_a.GetLength(1)];       // �Q�[���u���b�N�\���̏�����
        for(var r = 0; r < g_boxes.GetLength(0); r++)
        {
            for(var c = 0; c < g_boxes.GetLength(1); c++)
            {
                // -- �u���b�N���� -- //
                var box = Instantiate(gen_box);
                box.name = $"GimBox({r},{c})";
                box.transform.parent = pos_obj.transform;
                var spriteR = box.GetComponent<SpriteRenderer>();
                if(ref_a[r,c] == 0) { spriteR.sprite = snow; }
                if(ref_a[r,c] == 1) { spriteR.sprite = water; }
                box.transform.position = new Vector3(
                    pos_obj.transform.position.x + c * (spriteR.size.x + span),
                    pos_obj.transform.position.y - r * (spriteR.size.y + span),
                    pos_obj.transform.position.z
                    );
                // -- �Q�Ƃ��i�[ -- //
                g_boxes[r, c].gim_box_ = box;
                if(box.GetComponent<DieCuttingGimBox>() != null)
                g_boxes[r,c].gim_script_ = box.GetComponent <DieCuttingGimBox>();
                
                // -- setter -- //
                var scr = g_boxes[r, c].gim_script_;
                scr.State = ref_a[r, c];
                scr.Snow_img = snow_img_;
                scr.Water_img = water_img_;
                scr.Fire_tag_name = fire_tag_name_;
                scr.Ice_tag_name = ice_tag_name_;
                scr.Box_change_time = box_change_time_;
            }
        }
    }

    private void DestroyChildren(GameObject parent)
    {
        for (var i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            child.parent = null;
            GameObject.Destroy(child.gameObject);
        }
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
