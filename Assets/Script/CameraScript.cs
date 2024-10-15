using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//�J�����̓���Ɋւ���N���X
public class CameraScript : MonoBehaviour
{
    GameObject Player; //�v���C���[
    public bool auto; //�������X�N���[�����ۂ�
    int endPos, moveTime; //�J�����̃S�[���ʒu�A�ړ�����


    void Start()
    {
        Player = GameObject.Find("Player");
        endPos = 200;
        moveTime = 300;

        //�������X�N���[���Ȃ�A�J�����������ňړ�
        if(auto)
        {
            transform.DOMoveX(endPos, moveTime);
            
        }
    }


    void Update()
    {
        // �v���C���[�Ǐ]����
        if (!auto && Player.transform.position.x >= -1 && Player.transform.position.x <= 199)
        {
            transform.position = new Vector3(
                Player.transform.position.x + 1,
                transform.position.y,
                transform.position.z);
        }

    }

}
