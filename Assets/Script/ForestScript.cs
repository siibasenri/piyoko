using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背景の樹木に関するクラス
public class ForestScript : MonoBehaviour
{
    GameObject Player;
    // 
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // 
    void Update()
    {
        //画面範囲内ならプレイヤーの位置に追従する
        if (Player.transform.position.x >= -1 && Player.transform.position.x <= 199)
        {

            transform.position = new Vector3(
                Player.transform.position.x + 1,
                transform.position.y,
                transform.position.z);
        }
    }
}
