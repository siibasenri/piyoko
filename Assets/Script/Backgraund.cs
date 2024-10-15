using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背景画像をカメラに合わせて動かすクラス
public class Backgraund : MonoBehaviour
{
    GameObject Camera;

    //カメラを探す
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
    }

    //自分の位置をカメラの位置と合わせる
    void Update()
    {
        Vector2 pos = new Vector2(Camera.transform.position.x,Camera.transform.position.y);
        transform.position = pos;
    }
}
