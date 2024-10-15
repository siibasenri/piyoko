using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//�ړ����Ɋւ���N���X
public class moveFloarScript : MonoBehaviour
{
    GameObject emp;
    Vector2 moveFocus;
    float reverseTime;

    public int firstFocus = 1;
    public bool isVirtical = false;


    /*********************************
     * 
     *       �ړ�����
     * 
     * ******************************/
    void Start()
    {
        emp = transform.GetChild(0).gameObject;

        if (isVirtical)
        {
            this.transform.DOMove(new Vector3(transform.position.x, transform.position.y + firstFocus * 5f, transform.position.z), 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic).SetLink(gameObject);
        }

        else
        {
            this.transform.DOMove(new Vector3(transform.position.x + firstFocus * 5f, transform.position.y, transform.position.z), 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic).SetLink(gameObject);
        }
    }


    //�v���C���[�𑷃I�u�W�F�N�g�ɂ��āA����Ĉړ��ł���悤�ɂ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = emp.transform;
        }
    }

    
    //�v���C���[�̐e�q�֌W�������āA����Ĉړ��ł��Ȃ��悤�ɂ���
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
