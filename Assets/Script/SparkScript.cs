using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ΉԂɊւ���N���X
//����������ʒu�̗\���̂��߂ɌĂ΂��
public class SparkScript : MonoBehaviour
{
    SpriteRenderer sp;
    float time,sin;

    //Sprite���擾
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    //�_�ł��āA2�b��ɔj��
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
