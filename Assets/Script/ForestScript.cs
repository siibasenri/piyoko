using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�w�i�̎��؂Ɋւ���N���X
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
        //��ʔ͈͓��Ȃ�v���C���[�̈ʒu�ɒǏ]����
        if (Player.transform.position.x >= -1 && Player.transform.position.x <= 199)
        {

            transform.position = new Vector3(
                Player.transform.position.x + 1,
                transform.position.y,
                transform.position.z);
        }
    }
}
