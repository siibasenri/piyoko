using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//エビルプラントに関するクラス
public class EvilplantScript : MonoBehaviour
{
    Animator anim;
    CircleCollider2D head; 
    SpriteRenderer sp;
    AudioSource se;

    bool isDead;
    Vector3 pos1, pos2;

    void Start()
    {
        anim = GetComponent<Animator>();
        head = GetComponent<CircleCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        se = GetComponent<AudioSource>();
        isDead = false;
    }

    /******************************
     * 
     *    倒れたときの演出
     * 
     * ***************************/
    void Update()
    {
        if (isDead)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            sp.color = new Color(1f, 1f, 1f, level);
        }

        pos1 = transform.position + new Vector3(head.bounds.size.x * 0.45f, head.bounds.size.y * 0.9f, 0);
        pos2 = transform.position + new Vector3(head.bounds.size.x * -0.45f, head.bounds.size.y * 0.9f, 0);
        Debug.DrawLine(pos1, pos2);
    }

    //当たり判定処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player" )
        {
            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y > transform.position.y + head.bounds.size.y * 0.9f && 
                    p.point.x < transform.position.x + head.bounds.size.x * 0.45f && 
                    p.point.x > transform.position.x - head.bounds.size.x * 0.45f)
                {
                    
                    StartCoroutine("Dead");
                }
            }
        }
    }
    
       
    /********************************
     * 
     *    倒れた時の処理
     * 
     * *******************************/
    IEnumerator Dead()
    {
        head.enabled = false;
        
        isDead = true;

        anim.SetBool("isDead", isDead);

        se.Play();

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
