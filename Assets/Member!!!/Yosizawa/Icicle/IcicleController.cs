using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IcicleController : MonoBehaviour
{
    [Header("落下速度")]
    [SerializeField] float _fallSpeed = 1.0f;
    [Header("オーラ接触時の拡大・縮小倍率")]
    [SerializeField,Range(1f, 10f),Tooltip("ゲームオブジェクト の scale 変化倍率")] float _magnification = 1.0f;
    [Header("このゲームオブジェクトの最小の大きさ")]
    [SerializeField,Tooltip("ゲームオブジェクトの scale の最小")] float _minSize = 2.0f;
    [Header("このゲームオブジェクトの最大の大きさ")]
    [SerializeField, Tooltip("ゲームオブジェクトの scale の最大")] float _maxSize = 6.0f;
    [Header("GameObject消失時に再生するAnimetion")]
    [SerializeField] GameObject _onDestroyAnimation = default;
    [Header("Playerを検知する光線を 表示 or 非表示")]
    [SerializeField,Tooltip("Rayの表示・非表示の切り替え")] bool _isGizmo = false;
    [Header("Playerを検知する光線を飛ばす方向")]
    [SerializeField,Range(-5f, 5f),Tooltip("Rayを飛ばす方向")] float _direction = 1f;
    /// <summary>Rayの距離</summary>
    float _length = 50f;
    /// <summary>Rayを飛ばす向き</summary>
    Vector2 _dir = Vector2.zero;
    /// <summary>Rayを飛ばして当たったcolliderの情報</summary>
    RaycastHit2D _hit;
    Rigidbody2D _rb;

    void Start()
    {
        _magnification /= 100f;  //整数だと倍率が高すぎるので、あらかじめ低くしておく
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    void Update()
    {
        //Rayに当たったcolliderがPlayerかどうか判定する
        _hit = Physics2D.Raycast(transform.position, _dir, _length, LayerMask.GetMask("Player"));

        if(_hit.collider)  //RayがPlayerに当たった時の処理
        {
            _rb.gravityScale = _fallSpeed;
            Debug.Log("Check In");
        }
    }

    /// <summary>Rayを可視化するための関数</summary>
    void OnDrawGizmos()
    {
        if (_isGizmo == false) return;

        _dir = new Vector2(_direction, -1).normalized;
        Gizmos.DrawRay(transform.position, _dir * _length);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //ゲームオブジェクトの大きさの情報が入った変数
        var localScale = transform.localScale;

        //ゲームオブジェクトが極端に大きく(小さく)ならないようしておく
        localScale.x = Mathf.Clamp(localScale.x, _minSize, _maxSize);
        localScale.y = Mathf.Clamp(localScale.y, _minSize, _maxSize);

        //当たったオーラが「溶かす」ならゲームオブジェクトの大きさを減少
        if (collision.gameObject.tag is "Hot")
        {
            localScale.x -= _magnification;
            localScale.y -= _magnification;
            transform.localScale = localScale;
        }
        //当たったオーラが「固める」ならゲームオブジェクトの大きさを増加
        if (collision.gameObject.tag is "Cool")
        {
            localScale.x += _magnification;
            localScale.y += _magnification;
            transform.localScale = localScale;
        }
    }
}