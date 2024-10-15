using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//カメラの動作に関するクラス
public class CameraScript : MonoBehaviour
{
    GameObject Player; //プレイヤー
    public bool auto; //強制横スクロールか否か
    int endPos, moveTime; //カメラのゴール位置、移動時間


    void Start()
    {
        Player = GameObject.Find("Player");
        endPos = 200;
        moveTime = 300;

        //強制横スクロールなら、カメラを自動で移動
        if(auto)
        {
            transform.DOMoveX(endPos, moveTime);
            
        }
    }


    void Update()
    {
        // プレイヤー追従処理
        if (!auto && Player.transform.position.x >= -1 && Player.transform.position.x <= 199)
        {
            transform.position = new Vector3(
                Player.transform.position.x + 1,
                transform.position.y,
                transform.position.z);
        }

    }

}
