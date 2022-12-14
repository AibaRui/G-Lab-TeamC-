using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    /// <summary>プレイヤーにノックバックを与える</summary>
    /// <param name="bulletPosition">弾の位置を入れる</param>
    /// <param name="power">飛ばす力</param>
    void AddDamage(Transform bulletPosition, float power);
}
