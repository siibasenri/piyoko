using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//狐の挙動に関するクラス
//狐は、伏せをした後ジャンプ移動する
public class FoxScript : MonoBehaviour
{
    Rigidbody2D rg;
    SpriteRenderer sp;
    public Animator anim;
    public LayerMask groundLayer;
    float dir, timeCount;
    bool isWall, isGround, isCliff, jumping, waiting, jumpAfter;
    public bool isDead;


    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        dir = 1;
        jumping = waiting = jumpAfter = isDead = isCliff = false;
        timeCount = 0;

    }


    /************************************
     * 
     * 　　　ジャンプ移動の処理
     * 
     * ***********************************/
    private void FixedUpdate()
    {

        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 groundArea = new Vector2(0.5f, 0);

        Vector2 fallArea1 = new Vector2(-2 * dir, 2);
        Vector2 fallArea2 = new Vector2(-1 * dir, 1);

        Vector2 cliffArea1 = new Vector2(-3 * dir, 0);
        Vector2 cliffArea2 = new Vector2(-2 * dir, -3);

        Debug.DrawLine(groundPos + cliffArea1,
            groundPos + cliffArea2, Color.red);


        isWall = Physics2D.OverlapArea(
            groundPos + fallArea1,
            groundPos + fallArea2,
            groundLayer);

        isGround = Physics2D.OverlapArea(
            groundPos + groundArea,
            groundPos - groundArea,
            groundLayer);

        isCliff = Physics2D.OverlapArea(
            groundPos + cliffArea1,
            groundPos + cliffArea2,
            groundLayer);

        if (!isDead)
        {
            if (!isWall && isGround && !waiting) //ジャンプ処理。壁が無くて、地面に着いてて、伏せ中じゃない
            {
                //StartCoroutine("jump");
                anim.SetBool("isArea", true);

                if (jumpAfter)
                {
                    jumping = true;
                    jumpAfter = false;
                }

                if (!jumpAfter)
                {
                    Vector2 jumpForce = new Vector2(-100 * dir, 300);
                    rg.AddForce(jumpForce);
                    jumpAfter = true;
                }
            }

            if (jumping && isGround && Mathf.Abs(rg.velocity.y) > 0)
            {
                waiting = true;
                jumping = false;
            }

            if (waiting)
            {
                anim.SetBool("isArea", false);

                rg.velocity = Vector3.zero;

                if (isWall || !isCliff) 
                {
                    dir *= -1;
                    transform.localScale = new Vector3(dir, 1, 1);
                }

                timeCount += Time.deltaTime;

                if (timeCount >= 5)
                {
                    timeCount = 0;
                    waiting = false;
                }
            }
            anim.SetFloat("fallSpeed", rg.velocity.y);
        }

        if (isDead)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            sp.color = new Color(1f, 1f, 1f, level);
        }
    }
}
