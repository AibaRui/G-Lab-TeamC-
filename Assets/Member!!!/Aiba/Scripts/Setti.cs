using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setti : MonoBehaviour
{
    [SerializeField] GameObject _cure;

    [SerializeField] GameObject _box;
    Vector2 displayCenter;

    Vector3 screenToWorldPointPosition;

    // ブロックを設置する位置を一応リアルタイムで格納
    private Vector3 pos;

    Vector3 v;

    RaycastHit hit;

    bool a;

    GameObject _player;
    Vector3 position;

    void Start()
    {
        // ↓ 画面中央の平面座標を取得する
        displayCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        _player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        M();

        
        a = Physics.Raycast(_player.transform.position, v, out hit, 10);

        Debug.DrawRay(_player.transform.position,v, Color.green);

        if (a)
        {
            // ↓ 生成位置の変数の値を「ブロックの向き + ブロックの位置」


            // ↓ 右クリック
            if (Input.GetMouseButtonDown(1))
            {
                pos = hit.normal + hit.collider.transform.position;
                // 生成位置の変数の座標にブロックを生成
               var a =  Instantiate(_box);
                a.transform.position = pos;
            }

            // ↓ 左クリック
            if (Input.GetMouseButtonDown(0))
            {
                // ↓ レイが当たっているオブジェクトを削除
                Destroy(hit.collider.gameObject);
            }
        }

    }



    void M()
    {
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);



        v = new Vector3(position.x, position.y, _player.transform.position.z);
        _cure.transform.position = v;
    }
}
