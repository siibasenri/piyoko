using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背景の雲を動かすクラス
public class CloudScript : MonoBehaviour
{
    GameObject Camera;
    float beforepos, afterpos;


    //カメラの位置を把握する
    void Start()
    {
        Camera = GameObject.Find("Main Camera");

        beforepos = Camera.transform.position.x;
    }

    //遠くにある雲のように演出するため、少しだけカメラと同じ方向に動く
    void Update()
    {

        afterpos = Camera.transform.position.x;
        float deltamove = (beforepos - afterpos) * 0.99f;
        Vector2 pos = new Vector2(transform.position.x - deltamove, transform.position.y);
        transform.position = pos;


        beforepos = afterpos;

    }
}
