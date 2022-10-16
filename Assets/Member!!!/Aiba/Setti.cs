using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setti : MonoBehaviour
{
    [SerializeField] GameObject _box;
    Vector2 displayCenter;

    Vector3 screenToWorldPointPosition;

    // �u���b�N��ݒu����ʒu���ꉞ���A���^�C���Ŋi�[
    private Vector3 pos;

    Vector3 v;

    RaycastHit hit;

    bool a;

    GameObject _player;
    Vector3 position;

    void Start()
    {
        // �� ��ʒ����̕��ʍ��W���擾����
        displayCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        _player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        M();

        
        a = Physics.Raycast(_player.transform.position, v, out hit, 10);

        Debug.DrawRay(_player.transform.position,v, Color.green);

        if (a)
        {
            // �� �����ʒu�̕ϐ��̒l���u�u���b�N�̌��� + �u���b�N�̈ʒu�v


            // �� �E�N���b�N
            if (Input.GetMouseButtonDown(1))
            {
                pos = hit.normal + hit.collider.transform.position;
                // �����ʒu�̕ϐ��̍��W�Ƀu���b�N�𐶐�
               var a =  Instantiate(_box);
                a.transform.position = pos;
            }

            // �� ���N���b�N
            if (Input.GetMouseButtonDown(0))
            {
                // �� ���C���������Ă���I�u�W�F�N�g���폜
                Destroy(hit.collider.gameObject);
            }
        }

    }



    void M()
    {
        position = Input.mousePosition;
        // Z���C��
        position.z = 10f;
        // �}�E�X�ʒu���W���X�N���[�����W���烏�[���h���W�ɕϊ�����
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);


        v = new Vector3(position.x, position.y, _player.transform.position.z);
    }
}
