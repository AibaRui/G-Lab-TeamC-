using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeAura : MonoBehaviour
{
    [Header("Playerの子オブジェクトのパーティクルシステムをつける")]
    [SerializeField] ParticleController2 _particleController;

    [Header("オーラが変わる音")]
    [SerializeField] AudioSource _aud;

    int count = 0;

    [Header("===Joistickでの切り替え===")]

    // 中心点
    private Vector3 _center;

    // 円運動周期
    [Header("オーラの回転の速度。小さいほど早い")]
    [SerializeField] private float _period = 2;

    [Header("オーラが棒状の時のイラスト")]
    [Tooltip("オーラが棒状の時のイラスト")] [SerializeField] GameObject _imageStick;

    [Header("オーラが円の時のイラスト")]
    [Tooltip("オーラが円の時のイラスト")] [SerializeField] GameObject _imageCircle;


    [Header("上のオーラの位置")]
    [SerializeField] Transform _upPos;
    [Header("下のオーラの位置")]
    [SerializeField] Transform _downPos;
    [Header("右のオーラの位置")]
    [SerializeField] Transform _rightPos;
    [Header("左のオーラの位置")]
    [SerializeField] Transform _leftPos;

    AuraPos _auraPos = AuraPos.Up;

    bool _ischanging = false;
    bool _isCircleAura = true;

    void Start()
    {
        _imageCircle.SetActive(true);
    }

    void Update()
    {    
    }

    /// <summary>ジョイスティックでの切り替え</summary>
    /// ポジションを決める
   public void CheckChenge()
    {
        if (_ischanging)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            switch (_auraPos)
            {
                case AuraPos.Up:
                    pos = _upPos.position;
                    break;

                case AuraPos.Down:
                    pos = _downPos.position;
                    break;

                case AuraPos.Right:
                    pos = _rightPos.position;
                    break;

                case AuraPos.Left:
                    pos = _leftPos.position;
                    break;
            }
            Change(pos);
        }

    }

    /// <summary>ジョイスティックでの切り替え</summary>
    /// 回転させる処理
    public void Change(Vector3 nextTransform)
    {
        var tr = _imageStick.transform;
        float period = 0;

        //最短で回転する方向を決める    
        if (_auraPos == AuraPos.Up) period = nextTransform.x - tr.position.x >= 0 ? -_period : _period;
        else if (_auraPos == AuraPos.Down) period = nextTransform.x - tr.position.x >= 0 ? _period : -_period;
        else if (_auraPos == AuraPos.Right) period = nextTransform.y - tr.position.y >= 0 ? _period : -_period;
        else if (_auraPos == AuraPos.Left) period = nextTransform.y - tr.position.y >= 0 ? -_period : _period;

        // 回転のクォータニオン作成
        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, transform.forward);

        // 円運動の位置計算
        var pos = tr.position;

        _center = transform.position;

        pos -= _center;
        pos = angleAxis * pos;
        pos += _center;

        tr.position = pos;
        tr.rotation = tr.rotation * angleAxis;

        //回転終了の判定と、修正
        if (Vector2.Distance(nextTransform, tr.transform.position) < 0.3)
        {
            _aud.Play();
            _ischanging = false;
            tr.position = nextTransform;

            if (_auraPos == AuraPos.Up) tr.eulerAngles = new Vector3(0, 0, 0);
            else if (_auraPos == AuraPos.Down) tr.eulerAngles = new Vector3(0, 0, -180);
            else if (_auraPos == AuraPos.Right) tr.eulerAngles = new Vector3(0, 0, -90);
            else if (_auraPos == AuraPos.Left) tr.eulerAngles = new Vector3(0, 0, 90);
        }

    }

    /// <summary>ジョイスティックでの切り替え</summary>
    public void ChangeAuraControllerJoistick(int _playerNumber)
    {
        var h = Input.GetAxisRaw($"AuraChangeHorizontal{_playerNumber}");
        var v = Input.GetAxisRaw($"AuraChangeVertical{_playerNumber}");

        //キーボードの仮受付用
        var h1 = Input.GetAxisRaw("Horizontal1");
        var v1 = Input.GetAxisRaw("Vertical1");
        Debug.Log(h);
        if (Input.GetButtonDown($"AuraChangeClick{_playerNumber}"))
        {
            _particleController.AuraChange(0);
            _isCircleAura = !_isCircleAura;
            _imageCircle.SetActive(_isCircleAura);
            _imageStick.SetActive(!_isCircleAura);
            _aud.Play();

            if (_isCircleAura) _particleController.AuraChange(0);
            else _particleController.AuraChange(1);
        }



        if (!_isCircleAura)
        {
            if (v < 0 || v1 > 0)
            {
                _auraPos = AuraPos.Up;
                _ischanging = true;
                _particleController.AuraChange(1);
            }
            if (v > 0 || v1 < 0)
            {
                _auraPos = AuraPos.Down;
                _ischanging = true;
                _particleController.AuraChange(3);

            }
            if (h < 0 || h1 < 0)
            {
                _auraPos = AuraPos.Left;
                _ischanging = true;
                _particleController.AuraChange(4);
            }
            if (h > 0 || h1 > 0)
            {
                _auraPos = AuraPos.Right;
                _ischanging = true;
                _particleController.AuraChange(2);

            }
        }
    }



    //キーボードでの切り替え
    public void ChangeAuraKeyBord(int _playerNumber)
    {
        var h = 0;
        var v = 0;

        var h2 = 0;
        var v2 = 0;

        if (Input.GetKeyDown(KeyCode.R) && _playerNumber == 1)
        {
            _isCircleAura = !_isCircleAura;
            _imageCircle.SetActive(_isCircleAura);
            _imageStick.SetActive(!_isCircleAura);
            if (_isCircleAura) _particleController.AuraChange(0);
            else _particleController.AuraChange(1);
            _aud.Play();
        }
        if (Input.GetKeyDown(KeyCode.O) && _playerNumber == 2)
        {
            _isCircleAura = !_isCircleAura;
            _imageCircle.SetActive(_isCircleAura);
            _imageStick.SetActive(!_isCircleAura);
            if (_isCircleAura) _particleController.AuraChange(0);
            else _particleController.AuraChange(1);
            _aud.Play();
        }

        //Player1
        if (Input.GetKey(KeyCode.H) && _playerNumber == 1)
        {
            h = 1;
        }
        else if (Input.GetKey(KeyCode.F) && _playerNumber == 1)
        {
            h = -1;
        }
        if (Input.GetKey(KeyCode.T) && _playerNumber == 1)
        {
            v = -1;
        }
        else if (Input.GetKey(KeyCode.G) && _playerNumber == 1)
        {
            v = 1;
        }

        //Player2
        if (Input.GetKey(KeyCode.L) && _playerNumber == 2)
        {
            h2 = 1;
        }
        else if (Input.GetKey(KeyCode.J) && _playerNumber == 2)
        {
            h2 = -1;
        }
        if (Input.GetKey(KeyCode.I) && _playerNumber == 2)
        {
            v2 = 1;
        }
        else if (Input.GetKey(KeyCode.K) && _playerNumber == 2)
        {
            v2 = -1;
        }

        if (!_isCircleAura)
        {
            if (v < 0 || v2 > 0)
            {
                //_particleController.SetAuraChange(ParticleController.mode_.up_box);
                _auraPos = AuraPos.Up;
                _ischanging = true;
                _particleController.AuraChange(1);
            }
            if (v > 0 || v2 < 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.down_box);
                _auraPos = AuraPos.Down;
                _ischanging = true;
                _particleController.AuraChange(3);
            }
            if (h < 0 || h2 < 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.back_box);
                _auraPos = AuraPos.Left;
                _ischanging = true;
                _particleController.AuraChange(4);
            }
            if (h > 0 || h2 > 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.front_box);
                _auraPos = AuraPos.Right;
                _ischanging = true;
                _particleController.AuraChange(2);
            }
        }
    }






    enum AuraPos
    {
        Up,
        Down,
        Right,
        Left,
    }
}

