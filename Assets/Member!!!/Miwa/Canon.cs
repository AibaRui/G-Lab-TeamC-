using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject snowBall = null;
    [SerializeField] float interval = 1;
    private float _elapse;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        _elapse += Time.deltaTime;
        if(_elapse > interval)
        {
            var sb = Instantiate(snowBall);
            var spawn = gameObject.transform.position;
            sb.transform.position = spawn;
            _elapse = 0;

        }
    }
}
