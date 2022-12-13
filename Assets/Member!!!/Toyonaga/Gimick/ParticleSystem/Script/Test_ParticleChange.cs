using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ParticleChange : MonoBehaviour
{
    public ParticleController2 PcFire_;
    public ParticleController2 PcIce_;

    private int fire_num = 0;
    private int ice_num = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            fire_num++;
            if (fire_num >= 5) { fire_num = 0; }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ice_num++;
            if (ice_num >= 5) { ice_num = 0; }
        }

        PcFire_.AuraChange(fire_num);
        PcIce_.AuraChange(ice_num);
    }
}
