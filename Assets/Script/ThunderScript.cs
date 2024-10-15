using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//雷に関するクラス
public class ThunderScript : MonoBehaviour
{
    SpriteRenderer sp;
    float sin,time;
    Collider2D col;
    AudioSource se;

    // 初期化
    void Start()
    {
        se = GetComponent<AudioSource>();
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        se.Play();
    }

    //点滅したあと、2秒後に破壊
    void Update()
    {
        time += Time.deltaTime;
        sin = Mathf.Sin(Time.time*10);
        sp.color = new Color(1f, 1f, 1f, sin);

        if (time > 2)
        {
            Destroy(gameObject);
        }
    }
}
