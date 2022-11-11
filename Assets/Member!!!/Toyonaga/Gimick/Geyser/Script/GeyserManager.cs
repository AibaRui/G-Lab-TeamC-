using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserManager : MonoBehaviour
{
    [SerializeField, Header("‰ŠƒI[ƒ‰‚Ì–¼‘O‚ğ“o˜^")]
    private string fire_aura_;
    public string Fire_aura { get { return fire_aura_; } }
    [SerializeField, Header("•XƒI[ƒ‰‚Ì–¼‘O‚ğ“o˜^")]
    private string ice_aura_;
    public string Ice_aura { get { return ice_aura_; } }
    [SerializeField, Header("ƒ|[ƒY’†AGeyser‚Ì“®‚«‚ğ‘S‚Ä~‚ß‚é")]
    public bool is_Pause_ = false;
    [SerializeField, Header("•X‚ÌŠâ‚ª—n‚¯‚½‚©‚Ç‚¤‚©")]
    public bool is_IceRock_melted_ = false;
    
}
