using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�w�i�̉_�𓮂����N���X
public class CloudScript : MonoBehaviour
{
    GameObject Camera;
    float beforepos, afterpos;


    //�J�����̈ʒu��c������
    void Start()
    {
        Camera = GameObject.Find("Main Camera");

        beforepos = Camera.transform.position.x;
    }

    //�����ɂ���_�̂悤�ɉ��o���邽�߁A���������J�����Ɠ��������ɓ���
    void Update()
    {

        afterpos = Camera.transform.position.x;
        float deltamove = (beforepos - afterpos) * 0.99f;
        Vector2 pos = new Vector2(transform.position.x - deltamove, transform.position.y);
        transform.position = pos;


        beforepos = afterpos;

    }
}
