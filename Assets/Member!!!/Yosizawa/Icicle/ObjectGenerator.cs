using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("�����������Q�[���I�u�W�F�N�g��ݒ肷��")]
    private GameObject _go;
    [SerializeField, Tooltip("�ő哯�����ݐ�")]
    private int _maxOfExist = 0;
    /// <summary>�����Ԋu</summary>
    private float _interval = 1.0f;
    /// <summary>���Ԃ��v������^�C�}�[</summary>
    float _timer = 0f;

    private void Update()
    {
        //���݂̃I�u�W�F�N�g�̐����w�肵������葽��������A���������Ȃ�
        if (ExistCount() > _maxOfExist) return;

        _timer += Time.deltaTime;

        if (_timer >= _interval)
        {
            //���������I�u�W�F�N�g�𐔂��邽�߂ɁAGenerator�̎q�I�u�W�F�N�g�ɂ��Ă���
            GameObject go = Instantiate(_go);
            go.transform.SetParent(transform);
            go.transform.position = transform.position;
        }
    }

    /// <summary>���݁A�����̃I�u�W�F�N�g�����݂��Ă��邩�𒲂ׂ�</summary>
    /// <returns>���݁A���݂��Ă���I�u�W�F�N�g�̐�</returns>
    private int ExistCount()
    {
        int count = 0;

        foreach (Transform child in transform)
        {
            count++;
        }
        return count;
    }
}
