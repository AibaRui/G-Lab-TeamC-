using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    /// <summary>�v���C���[�Ƀm�b�N�o�b�N��^����</summary>
    /// <param name="bulletPosition">�e�̈ʒu������</param>
    /// <param name="power">��΂���</param>
    void AddDamage(Transform bulletPosition, float power);
}
