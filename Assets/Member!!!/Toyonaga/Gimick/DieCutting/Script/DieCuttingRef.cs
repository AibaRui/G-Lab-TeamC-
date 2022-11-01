using UnityEngine;

public class DieCuttingRef : MonoBehaviour
{
    [SerializeField, Tooltip("��Q�Ɨp�摜�A�^�b�` �� �{�X�N���v�g����(�����o�ϐ�)��int [,] �z��� 0 �Ŏw�肵�ĉ�����")]
    private Sprite im_snow_;
    [SerializeField, Tooltip("���Q�Ɖ摜�A�^�b�` �� �{�X�N���v�g����(�����o�ϐ�)��int [,] �z��� 1 �Ŏw�肵�ĉ�����")]
    private Sprite im_water_;
    [SerializeField, Tooltip("�摜���m�̊Ԋu���J����ꍇ�A�l���w��")]
    float im_span_ = 0f;

    private static GameObject[,] boxes_;       // �摜�Q�[���I�u�W�F�N�g

    // ---- �v�ҏW�F��摜�F0, ���摜 : 1�ŏ����� ---- //
    private int[,] boxes_state_ = new int [4, 4]
    {
        {0, 0, 0, 0, },
        {0, 0, 0, 0, },
        {1, 0, 0, 0, },
        {1, 0, 0, 0, },
    };                       // �Q�Ɖ摜�̕��т��X�N���v�g����w�肷��
    public int[,] Boxes_state { get { return boxes_state_; } }  // getter()

    private void Start()
    {
        // ---- ���Ɏq�I�u�W�F�N�g�����݂���ꍇ�F�폜 --- //
        DestroyChildren(this.gameObject);
        // ---- int[,]�z�������ɉ摜�z��`�� ---- //
        boxes_ = new GameObject[boxes_state_.GetLength(0), boxes_state_.GetLength(1)];
        for (var r = 0; r < boxes_state_.GetLength(0); r++)
        {
            for (var c = 0; c < boxes_state_.GetLength(1); c++)
            {
                boxes_[r, c] = new GameObject($"Box({r},{c})");
                boxes_[r, c].transform.parent = transform;                      // �q�I�u�W�F�N�g�ɂ���
                var sprite = boxes_[r, c].AddComponent<SpriteRenderer>();       // �摜�\���̂��߃����_�[��^����
                if (boxes_state_[r, c] == 0) { sprite.sprite = im_snow_; }      // �摜��^����
                else if (boxes_state_[r, c] == 1) { sprite.sprite = im_water_; }
                else { Debug.Log("Error : �� or ���摜�� 0 or 1 �̐����Ŏw�肵�ĉ�����"); }
                boxes_[r, c].transform.position = new Vector3(                  // �e���W����̈ړ������w��
                    transform.position.x + c * (sprite.size.x + im_span_),
                    transform.position.y - r * (sprite.size.y + im_span_),
                    transform.position.z);
            }
        }
    }

    private void DestroyChildren(GameObject parent)
    {
        for(var i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            child.parent = null;
            GameObject.Destroy(child.gameObject);
        }
    }

   
}
