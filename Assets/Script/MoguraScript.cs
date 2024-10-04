using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoguraScript : MonoBehaviour
{
    GameObject Player;
    Animator anim;
    BoxCollider2D col;
    SpriteRenderer sp;
    AudioSource se;
    bool isDead;
    float dis;

    //初期
    void Start()
    {
        Player = GameObject.Find("Player");

        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        se = GetComponent<AudioSource>();

        dis = 0;
        isDead = false;
        col.enabled = false;
    }


    void Update()
    {
        dis = Vector3.Distance(Player.transform.position, transform.position);

        //プレイヤーが近づいたときに頭を出す
        if (dis < 6 && !isDead)
        {
            anim.SetFloat("dis", dis);
            col.enabled = true;
        }
        
        //死亡時の点滅演出
        if (isDead)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            sp.color = new Color(1f, 1f, 1f, level);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.name == "Player")
        {
            foreach (ContactPoint2D p in collision.contacts)
            {
                //プレイヤーが頭を踏んだとき
                if ( p.point.x < transform.position.x + col.bounds.size.x * 0.5f && 
                     p.point.x > transform.position.x - col.bounds.size.x * 0.5f)
                {

                    StartCoroutine("Dead");
                }
            }
        }

    }

    //死んだときの処理
    private IEnumerator Dead()
    {

        isDead = true;

        col.enabled = false;

        anim.SetBool("isDead", isDead);

        se.Play();

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

}
