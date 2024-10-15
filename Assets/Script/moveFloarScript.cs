using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//移動床に関するクラス
public class moveFloarScript : MonoBehaviour
{
    GameObject emp;
    Vector2 moveFocus;
    float reverseTime;

    public int firstFocus = 1;
    public bool isVirtical = false;


    /*********************************
     * 
     *       移動処理
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


    //プレイヤーを孫オブジェクトにして、乗って移動できるようにする
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = emp.transform;
        }
    }

    
    //プレイヤーの親子関係を消して、乗って移動できないようにする
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
