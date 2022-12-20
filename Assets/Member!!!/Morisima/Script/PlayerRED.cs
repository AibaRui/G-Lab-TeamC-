using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRED : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player ÇÃä»à’ìIÇ»ìÆçÏ
        if (Input.GetKey(KeyCode.W)) { this.transform.Translate(0, 0.1F, 0); }
        if (Input.GetKey(KeyCode.A)) { this.transform.Translate(-0.01F, 0, 0); }
        if (Input.GetKey(KeyCode.S)) { this.transform.Translate(0, -0.01F, 0); }
        if (Input.GetKey(KeyCode.D)) { this.transform.Translate(0.01F, 0, 0); }
    }
}
