using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ɋւ���N���X
public class ThunderScript : MonoBehaviour
{
    SpriteRenderer sp;
    float sin,time;
    Collider2D col;
    AudioSource se;

    // ������
    void Start()
    {
        se = GetComponent<AudioSource>();
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        se.Play();
    }

    //�_�ł������ƁA2�b��ɔj��
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
