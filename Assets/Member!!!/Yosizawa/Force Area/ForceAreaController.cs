using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ForceAreaController : GimickBase
{
    private BoxCollider2D _boxCol;
    private AreaEffector2D _effecter;

    private void Start()
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
