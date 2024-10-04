using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinScript : MonoBehaviour
{
    AudioSource se;
    float sin;

    void Start()
    {
        se = GetComponent<AudioSource>();
        sin = 0;
    }


    /**************************************************
     * 
     *                浮遊演出
     * 
     * **********************************************/
    void Update()
    {
        sin = Mathf.Sin(Time.time * 2);
        transform.localScale = new Vector3(sin, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + sin * 0.001f, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            StartCoroutine("Get");
        }
    }    


    /*************************************************************
     * 
     *      プレイヤーがコインを手に入れたときの処理
     * 
     * **********************************************************/
    IEnumerator Get()
    {
        se.Play();

        GetComponent<Collider2D>().enabled = false;

        Vector3 pos = transform.localPosition + Vector3.up * 1.5f;

        transform.DOLocalMove(pos, 0.5f).SetLink(gameObject);
        
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

}
