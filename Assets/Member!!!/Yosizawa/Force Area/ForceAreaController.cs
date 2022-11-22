using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ForceAreaController : GimickBase
{
    private BoxCollider2D _boxCol;

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider2D>();
    }
    public override void GameOverPause()
    {
        _boxCol.enabled = false;
    }

    public override void Pause()
    {
        _boxCol.enabled = false;
    }

    public override void Resume()
    {
        _boxCol.enabled = true;
    }
}
