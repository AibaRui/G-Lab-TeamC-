using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playr1Move : MonoBehaviour
{
    [SerializeField] float _moveSpeed=3;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();   
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 velo = new Vector3(h * _moveSpeed, _rb.velocity.y, 0);
        _rb.velocity = velo;
        
    }
}
