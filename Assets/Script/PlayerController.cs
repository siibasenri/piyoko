using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rg;
    SpriteRenderer sp;
    Animator anim;
    AudioSource se;
    Collider2D col;

    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip damageSE;

    int HP;
    float moveFocus, movePower, jumpPower;
    GameObject canvas;
    bool isJump, isDamage, isGround, isFall;

    public LayerMask groundLayer;

    Vector3 pos1, pos2;

    /****************************************
     * 
     *            初期化
     * 
     * ************************************/
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        se = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();

        canvas = GameObject.Find("Canvas");

        HP = 3;
        jumpPower = 400;
        movePower = 25;

        isJump = isDamage = isGround = isFall = false;

    }

    /********************************************
     * 
     *      入力受付、状態管理
     * 
     * ********************************************/

    void Update()
    {
        moveFocus = Input.GetAxis("Horizontal"); //右矢印で1,左矢印なら-1,どちらでもないなら0

        //キャラクターの向きを変更
        if (moveFocus > 0) transform.localScale = new Vector3(1, 1, 1);
        if (moveFocus < 0) transform.localScale = new Vector3(-1, 1, 1);

        //地面に接しているかの判定
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 groundArea = new Vector2(0.5f * moveFocus, 0.5f);

        isGround = Physics2D.OverlapArea(
            groundPos + groundArea,
            groundPos - groundArea,
            groundLayer);

        //地面に接しているときの処理
        if (isGround)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        //空中にいるときの処理
        if (!isGround && !isDamage)
        {
            if (rg.velocity.y > 0.01f)//上昇中
            {
                anim.SetBool("isJump", true);
            }

            if (rg.velocity.y < -0.1f)//下降中
            {
                anim.SetBool("isFall", true);
            }
        }

        //ジャンプ入力受付
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;  //FixedUpdateで処理
        }

        //落下ゲームオーバーの処理
        if (!isFall && transform.position.y < -10 && HP>0)
        {
            isFall = true;
            canvas.SendMessage("gameOverFadeOut");
        }
    }

    /***********************************************
     * 
     *               移動処理
     * 
     * *********************************************/
    void FixedUpdate()
    {
        //最高速度
        float maxSpeed = 5;

        //移動処理(ダメージ中以外)
        if (!isDamage)
        {
            rg.AddForce(transform.right * movePower * (moveFocus * maxSpeed - rg.velocity.x));
        }

        //ジャンプ処理
        if (isJump)
        {
            se.PlayOneShot(jumpSE);
            rg.AddForce(transform.up * jumpPower);

            isJump = false;
        }

        pos1 = transform.position + new Vector3(col.bounds.size.x * 0.45f, col.bounds.size.y * 0.1f, 0);
        pos2 = transform.position + new Vector3(col.bounds.size.x * -0.45f, col.bounds.size.y * 0.1f, 0);
        Debug.DrawLine(pos1, pos2,Color.red);
    }


    /******************************************************************
     * 
     * 　　　               衝突処理
     * 
     * *****************************************************************/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //コインに当たったら
        if (collision.transform.tag == "coin")
        {
            canvas.SendMessage("scoreAdd", 10);
        }

        //ゴール地点に着いたら
        if (collision.transform.tag == "ClearPoint")
        {
            canvas.SendMessage("gameClearFadeOut");
            Destroy(this);
        }

        //雷に当たった時
        if(collision.transform.tag =="Thunder")
        {
            StartCoroutine("Damage");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Enemy")
        {
            foreach (ContactPoint2D p in collision.contacts)
            {
                //敵の頭を踏んで倒したとき
                if (/*p.point.y < transform.position.y + col.bounds.size.y * 0.1f  &&*/
                    p.point.x < transform.position.x + col.bounds.size.x * 0.45f &&
                    p.point.x > transform.position.x - col.bounds.size.x * 0.45f)
                {
                    rg.velocity = Vector2.zero;
                    rg.AddForce(new Vector2(0, 100));
                }
                
                //敵に当たったとき
                else
                {
                    StartCoroutine("Damage");
                }
                //Debug.Log(p.point.x + " : " + transform.position.x);
            }
        }

        //台風に当たったとき
        if (collision.transform.tag == "Typhoon")
        {
            canvas.SendMessage("gameOverFadeOut");
            Destroy(this);
        }
    }



    /**********************************
     * 
     *   　　ダメージ処理
     *   　　
     ***********************************/
    IEnumerator Damage()
    {
        isDamage = true;

        anim.SetBool("isDamage", isDamage);

        rg.velocity = Vector2.zero;

        se.PlayOneShot(damageSE);

        canvas.SendMessage("lifeLoss", HP);

        HP--;

        //HPが残ってたら
        if (HP > 0)
        {
            //ノックバック
            rg.AddForce(new Vector2(-1 * Mathf.Sign(transform.localScale.x) * 200, 200));

            //ダメージ演出で半透明に
            sp.color = new Color(1f, 1f, 1f, 0.5f);

            yield return new WaitForSeconds(1);

            sp.color = new Color(1f, 1f, 1f, 1);

            isDamage = false;

            anim.SetBool("isDamage", isDamage);
        }

        //HPが0になったら
        else
        {
            rg.AddForce(new Vector2(0, 300));

            GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(0.5f);

            //ゲームオーバー演出
            canvas.SendMessage("gameOverFadeOut");
        }
    }
}
