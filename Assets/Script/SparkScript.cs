using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//火花に関するクラス
//雷が落ちる位置の予測のために呼ばれる
public class SparkScript : MonoBehaviour
{
    SpriteRenderer sp;
    float time,sin;

    //Spriteを取得
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    //点滅して、2秒後に破壊
    void Update()
    {
        time += Time.deltaTime;
        sin = Mathf.Sin(Time.time * 10);
        sp.color = new Color(1f, 1f, 1f, sin*0.5f);

        if (time > 2)
        {
            Destroy(gameObject);
        }
    }
}
