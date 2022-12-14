using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour, IDamagable
{
    [SerializeField] float _knockBackTime = 1;


    private float _countTime;

    Rigidbody2D _rb;

    private bool _isKnockBack;
    public bool IsKnockBack { get => _isKnockBack; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    public void AddDamage(Transform bulletPosition, float power)
    {
        _isKnockBack = true;
        Vector2 dir = bulletPosition.position - transform.position;
        if (dir.x >= 0)
        {
            _rb.AddForce(-dir.normalized * power, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(dir.normalized * power, ForceMode2D.Impulse);
        }
    }

    public void CountTime()
    {
        if (_isKnockBack)
        {
            _countTime += Time.deltaTime;
            if (_countTime > _knockBackTime)
            {
                _isKnockBack = false;
                _countTime = 0;
            }

        }
    }

}
