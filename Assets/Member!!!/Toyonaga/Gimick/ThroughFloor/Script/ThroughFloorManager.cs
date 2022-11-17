using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughFloorManager : MonoBehaviour
{
    [Header("--- ���ӁF�{�X�N���v�g����摜���R���C�_�T�C�Y�ꊇ�ύX ---")]
    [SerializeField, Header("����ʂ����̉����T�C�Y�w��")]
    private float width_ = 2.0f;
    [SerializeField, Header("����ʂ����̏c���T�C�Y�w��")]
    private float height_ = 0.2f;
    [SerializeField, Header("����ʂ����摜�o�^")]
    private SpriteRenderer through_floor_;
    [SerializeField, Header("����ʂ����R���C�_�o�^")]
    private BoxCollider2D box_collider_;
    
    private void OnValidate()
    {
        // ----- �C���X�y�N�^�r���[�̒l�X�V���Ɋ֐����{ ----- //
        // ---- �X�N���v�g����摜���R���C�_�̃T�C�Y���� ---- //
        through_floor_.size = new Vector2(width_, height_);
        box_collider_.size = new Vector2(width_, height_);
    }
}
