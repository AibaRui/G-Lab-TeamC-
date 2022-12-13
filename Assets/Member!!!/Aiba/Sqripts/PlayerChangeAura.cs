using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeAura : MonoBehaviour
{
    [Header("オーラの切り替えの操作を何でするか")]
    [SerializeField] Sousa _sousa = Sousa.Joistick;

    [Header("プレイヤーの番号")]
    [Tooltip("プレイヤーの番号")] [SerializeField] int _playerNumber = 1;

    [SerializeField] ParticleController _particleController;

    //[Header("===ボタンでの切り替え===")]

    //[Header("能力の形")]
    //[Tooltip("能力の形")] [SerializeField] GameObject[] _eria = new GameObject[1];

    //[Header("能力の形、上")]
    //[Tooltip("能力の形、上")] [SerializeField] GameObject _eriaUp;

    //[Header("能力の形、下")]
    //[Tooltip("能力の形、下")] [SerializeField] GameObject _eriaDown;
    
    //[Header("能力の形、右")]
    //[Tooltip("能力の形、右")] [SerializeField] GameObject _eriaRight;

    //[Header("能力の形、左")]
    //[Tooltip("能力の形、左")] [SerializeField] GameObject _eriaLeft;

    int count = 0;

    [Header("===Joistickでの切り替え===")]

    // 中心点
    [SerializeField] private Vector3 _center;

    // 円運動周期
    [SerializeField] private float _period = 2;


    [Header("棒状の形")]
    [Tooltip("棒状の形")] [SerializeField] GameObject _imageStick;

    [Header("円形")]
    [Tooltip("円形")] [SerializeField] GameObject _imageCircle;


    [Header("上のオーラの位置")]
    [SerializeField] Transform _upPos;
    [Header("下のオーラの位置")]
    [SerializeField] Transform _downPos;
    [Header("右のオーラの位置")]
    [SerializeField] Transform _rightPos;
    [Header("左のオーラの位置")]
    [SerializeField] Transform _leftPos;

    AuraPos _auraPos = AuraPos.Up;
    Vector3 _pos;
    bool _ischanging = false;
    bool _isCircleAura = true;

    void Start()
    {
        _pos = _upPos.position;
       // if (_sousa == Sousa.Bbuttun || _sousa == Sousa.KeyBord) ;
            // _eriaUp.SetActive(true);
           // _eria[count].SetActive(true);
    }

    void Update()
    {
       // if (_sousa == Sousa.Bbuttun)
       // {
            //ChangeAuraControllerButtun();
      //  }
        if (_sousa == Sousa.Joistick)
        {
            CheckChenge();
            ChangeAuraControllerJoistick();
        }
        else if (_sousa == Sousa.KeyBord)
        {
            ChangeAuraKeyBord();
            CheckChenge();
        }


    }

    /// <summary>ジョイスティックでの切り替え</summary>
    /// ポジションを決める
    void CheckChenge()
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
    void Change(Vector3 nextTransform)
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
            _ischanging = false;
            tr.position = nextTransform;

            if (_auraPos == AuraPos.Up) tr.eulerAngles = new Vector3(0, 0, 0);
            else if (_auraPos == AuraPos.Down) tr.eulerAngles = new Vector3(0, 0, -180);
            else if (_auraPos == AuraPos.Right) tr.eulerAngles = new Vector3(0, 0, -90);
            else if (_auraPos == AuraPos.Left) tr.eulerAngles = new Vector3(0, 0, 90);
        }

    }

    /// <summary>ジョイスティックでの切り替え</summary>
    void ChangeAuraControllerJoistick()
    {
        var h = Input.GetAxisRaw($"AuraChangeHorizontal{_playerNumber}");
        var v = Input.GetAxisRaw($"AuraChangeVertical{_playerNumber}");

        //キーボードの仮受付用
        var h1 = Input.GetAxisRaw("Horizontal1");
        var v1 = Input.GetAxisRaw("Vertical1");
        Debug.Log(h);
        if (Input.GetButtonDown($"AuraChangeClick{_playerNumber}") || Input.GetKeyDown(KeyCode.O))
        {

            _isCircleAura = !_isCircleAura;
            _imageCircle.SetActive(_isCircleAura);
            _imageStick.SetActive(!_isCircleAura);
        }



        if (!_isCircleAura)
        {
            if (v < 0 || v1 > 0)
            {
                //_particleController.SetAuraChange(ParticleController.mode_.up_box);
                _auraPos = AuraPos.Up;
                _ischanging = true;

            }
            if (v > 0 || v1 < 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.down_box);
                _auraPos = AuraPos.Down;
                _ischanging = true;

            }
            if (h < 0 || h1 < 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.back_box);
                _auraPos = AuraPos.Left;
                _ischanging = true;

            }
            if (h > 0 || h1 > 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.front_box);
                _auraPos = AuraPos.Right;
                _ischanging = true;

            }
        }
    }

    /// <summary>ボタンでの切り替え</summary>
    //void ChangeAuraControllerButtun()
    //{
    //    if (Input.GetButtonDown($"AuraChange{_playerNumber}"))
    //    {
    //        _eria[count].SetActive(false);
    //        count++;
    //        if (count == _eria.Length)
    //        {
    //            count = 0;
    //        }
    //        _eria[count].SetActive(true);
    //    }
    //}

    //キーボードでの切り替え
    void ChangeAuraKeyBord()
    {
        var h = 0;
        var v = 0;

        var h2 = 0;
        var v2 = 0;

        if (Input.GetKeyDown(KeyCode.R) && _playerNumber==1)
        {
            _isCircleAura = !_isCircleAura;
            _imageCircle.SetActive(_isCircleAura);
            _imageStick.SetActive(!_isCircleAura);
        }
        if (Input.GetKeyDown(KeyCode.O) && _playerNumber == 2)
        {
            _isCircleAura = !_isCircleAura;
            _imageCircle.SetActive(_isCircleAura);
            _imageStick.SetActive(!_isCircleAura);
        }
        
        //Player1
        if(Input.GetKey(KeyCode.H) && _playerNumber == 1)
        {
            h = 1;
        }
        else if(Input.GetKey(KeyCode.F) && _playerNumber == 1)
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

            }
            if (v > 0 || v2 < 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.down_box);
                _auraPos = AuraPos.Down;
                _ischanging = true;

            }
            if (h < 0 || h2 < 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.back_box);
                _auraPos = AuraPos.Left;
                _ischanging = true;

            }
            if (h > 0 || h2 > 0)
            {
                // _particleController.SetAuraChange(ParticleController.mode_.front_box);
                _auraPos = AuraPos.Right;
                _ischanging = true;

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


    enum Sousa
    {
        Joistick,
        //Bbuttun,
        KeyBord,
    }

}

