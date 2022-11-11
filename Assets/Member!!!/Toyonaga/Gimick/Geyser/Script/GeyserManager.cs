using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserManager : MonoBehaviour
{
    [SerializeField, Header("炎オーラの名前を登録")]
    private string fire_aura_;
    public string Fire_aura { get { return fire_aura_; } }
    [SerializeField, Header("氷オーラの名前を登録")]
    private string ice_aura_;
    public string Ice_aura { get { return ice_aura_; } }
    [SerializeField, Header("ポーズ中、Geyserの動きを全て止める")]
    public bool is_Pause_ = false;
    [SerializeField, Header("氷の岩が溶けたかどうか")]
    public bool is_IceRock_melted_ = false;
    
}
