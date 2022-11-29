using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledController : MonoBehaviour
{
    [SerializeField, Tooltip("オブジェクトの加速度")]
    private float _acceleration = 1f;
    [SerializeField, Tooltip("オブジェクトの最大速度")]
    private float _maxVelocity = 1f;
    [SerializeField, Tooltip("実際に動かすオブジェクト")]
    private GameObject _movingObj;
    /// <summary>assignされているオブジェクトのRigidbody2D</summary>
    private Rigidbody2D _objRb;
    ///// <summary>オブジェクトの速度</summary>
    private Vector2 _velocity;

    /// <summary>メンバー変数である_maxVelocityのプロパティ(読み取り専用)</summary>
    public float MaxVelocity => _maxVelocity;

    /// <summary>メンバー変数である_velocityのプロパティ</summary>
    public Vector2 Velocity
    {
        private set
        {
            value.x = Mathf.Clamp(_velocity.x, 0f, _maxVelocity);
            value.y = 0;
            _velocity = value;
        }
        get { return _velocity; }
    }

    private void Start()
    {
        //_movingObjと一緒に動いてほしいので、_movingObjの子オブジェクトにしておく
        transform.SetParent(_movingObj.transform);

        //SledBodyControllerがアタッチされていることを確約する
        if (!_movingObj.GetComponent<SledBodyController>())
        {
            _movingObj.AddComponent<SledBodyController>();
        }
        _objRb = _movingObj.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //[溶かす]オーラが当たったら、加速する
        if (collision.gameObject.tag is "Hot")
        {
            _velocity = new Vector2(_objRb.velocity.x * _acceleration, 0);
        }

        //[固める]オーラが当たったら、減速する
        if (collision.gameObject.tag is "Cool")
        {
            _velocity = new Vector2(_objRb.velocity.x * -_acceleration, 0);
        }
    }
}
