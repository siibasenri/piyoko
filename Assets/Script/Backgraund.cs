using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�w�i�摜���J�����ɍ��킹�ē������N���X
public class Backgraund : MonoBehaviour
{
    GameObject Camera;

    //�J������T��
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
    }

    //�����̈ʒu���J�����̈ʒu�ƍ��킹��
    void Update()
    {
        Vector2 pos = new Vector2(Camera.transform.position.x,Camera.transform.position.y);
        transform.position = pos;
    }
}
