using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PauseManager;

public class Canon : GimickBase
{
    [SerializeField] private GameObject snowBall = null;
    [SerializeField] float interval = 1;
    private float _elapse;
    bool isPause;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPause == false)
        {
            _elapse += Time.deltaTime;
            if (_elapse > interval)
            {
                var sb = Instantiate(snowBall);
                var spawn = gameObject.transform.position;
                sb.transform.position = spawn;
                _elapse = 0;

            }
        }
    }

    public override void GameOverPause()
    {
        isPause = true;
    }

    public override void Pause()
    {
        isPause = true;
    }

    public override void Resume()
    {
        isPause = false;
    }
}
