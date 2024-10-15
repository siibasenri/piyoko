using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//リスに関するクラス
public class RisuScript : MonoBehaviour
{
    CapsuleCollider2D body;
    Rigidbody2D rg;
    Animator anim;
    AudioSource se;
    SpriteRenderer sp;
    bool isDead,isWall,isEnemy;　//死亡判定、壁が前にあるか判定、敵が前にいるか判定
    float speed = 3f;

    public LayerMask groundLayer;
    Vector2 head1Pos,head2Pos; //進行方向に障害物があるかの判定用


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<CapsuleCollider2D>();
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        se = GetComponent<AudioSource>();
        sp = GetComponent<SpriteRenderer>();

        isDead = isWall = isEnemy = false;
        
    }

    //壁にぶつかるまで左右に移動する
    void Update()
    {

        head1Pos = transform.position + new Vector3(-1 * transform.localScale.x * body.bounds.size.x * 0.6f, 0.5f, 0);
        head2Pos = transform.position + new Vector3(-1 * transform.localScale.x * body.bounds.size.x * 0.6f, 0.3f, 0);

        if (!isDead)
        {
            float currentSpeed = speed - rg.velocity.magnitude;
            rg.AddForce(new Vector2(Mathf.Sign(transform.localScale.x) * -1 * currentSpeed, 0));


            isWall = Physics2D.OverlapArea(head1Pos,head2Pos,groundLayer);
            isEnemy = Physics2D.OverlapArea(head1Pos, head2Pos);

            //壁か敵が目の前にいるなら進行方向を反転
            if (isWall || isEnemy)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            }
        }
        //死亡時、点滅する
        else
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            sp.color = new Color(1f, 1f, 1f, level);
        }
    }


    //当たり判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y > transform.position.y + body.bounds.size.y * 0.9f && 
                    p.point.x < transform.position.x + body.bounds.size.x * 0.45f && 
                    p.point.x > transform.position.x - body.bounds.size.x * 0.45f)
                {
                    StartCoroutine("Dead");
                }
            }
        }
    }

    //倒された時の処理
    private IEnumerator Dead()
    {
        isDead = true;

        body.enabled = false;

        anim.SetBool("isDead", isDead);

        se.Play();

        rg.velocity = Vector2.zero;
        rg.bodyType = RigidbodyType2D.Kinematic;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
