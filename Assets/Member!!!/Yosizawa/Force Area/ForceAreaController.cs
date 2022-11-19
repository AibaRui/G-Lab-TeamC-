using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAreaController : GimickBase
{
    BoxCollider2D _boxCol;
    AreaEffector2D _effecter;

    void Start()
    {
        _boxCol = GetComponent<BoxCollider2D>();
        _effecter = GetComponent<AreaEffector2D>();
    }
    public override void GameOverPause()
    {
        _boxCol.enabled = false;
        _effecter.enabled = false;
    }

    public override void Pause()
    {
        _boxCol.enabled = false;
        _effecter.enabled = false;
    }

    public override void Resume()
    {
        _boxCol.enabled = true;
        _effecter.enabled = true;
    }
}
