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
     *            ������
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
     *      ���͎�t�A��ԊǗ�
     * 
     * ********************************************/

    void Update()
    {
        moveFocus = Input.GetAxis("Horizontal"); //�E����1,�����Ȃ�-1,�ǂ���ł��Ȃ��Ȃ�0

        //�L�����N�^�[�̌�����ύX
        if (moveFocus > 0) transform.localScale = new Vector3(1, 1, 1);
        if (moveFocus < 0) transform.localScale = new Vector3(-1, 1, 1);

        //�n�ʂɐڂ��Ă��邩�̔���
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 groundArea = new Vector2(0.5f * moveFocus, 0.5f);

        isGround = Physics2D.OverlapArea(
            groundPos + groundArea,
            groundPos - groundArea,
            groundLayer);

        //�n�ʂɐڂ��Ă���Ƃ��̏���
        if (isGround)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        //�󒆂ɂ���Ƃ��̏���
        if (!isGround && !isDamage)
        {
            if (rg.velocity.y > 0.01f)//�㏸��
            {
                anim.SetBool("isJump", true);
            }

            if (rg.velocity.y < -0.1f)//���~��
            {
                anim.SetBool("isFall", true);
            }
        }

        //�W�����v���͎�t
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;  //FixedUpdate�ŏ���
        }

        //�����Q�[���I�[�o�[�̏���
        if (!isFall && transform.position.y < -10 && HP>0)
        {
            isFall = true;
            canvas.SendMessage("gameOverFadeOut");
        }
    }

    /***********************************************
     * 
     *               �ړ�����
     * 
     * *********************************************/
    void FixedUpdate()
    {
        //�ō����x
        float maxSpeed = 5;

        //�ړ�����(�_���[�W���ȊO)
        if (!isDamage)
        {
            rg.AddForce(transform.right * movePower * (moveFocus * maxSpeed - rg.velocity.x));
        }

        //�W�����v����
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
     * �@�@�@               �Փˏ���
     * 
     * *****************************************************************/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�R�C���ɓ���������
        if (collision.transform.tag == "coin")
        {
            canvas.SendMessage("scoreAdd", 10);
        }

        //�S�[���n�_�ɒ�������
        if (collision.transform.tag == "ClearPoint")
        {
            canvas.SendMessage("gameClearFadeOut");
            Destroy(this);
        }

        //���ɓ���������
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
                //�G�̓��𓥂�œ|�����Ƃ�
                if (/*p.point.y < transform.position.y + col.bounds.size.y * 0.1f  &&*/
                    p.point.x < transform.position.x + col.bounds.size.x * 0.45f &&
                    p.point.x > transform.position.x - col.bounds.size.x * 0.45f)
                {
                    rg.velocity = Vector2.zero;
                    rg.AddForce(new Vector2(0, 100));
                }
                
                //�G�ɓ��������Ƃ�
                else
                {
                    StartCoroutine("Damage");
                }
                //Debug.Log(p.point.x + " : " + transform.position.x);
            }
        }

        //�䕗�ɓ��������Ƃ�
        if (collision.transform.tag == "Typhoon")
        {
            canvas.SendMessage("gameOverFadeOut");
            Destroy(this);
        }
    }



    /**********************************
     * 
     *   �@�@�_���[�W����
     *   �@�@
     ***********************************/
    IEnumerator Damage()
    {
        isDamage = true;

        anim.SetBool("isDamage", isDamage);

        rg.velocity = Vector2.zero;

        se.PlayOneShot(damageSE);

        canvas.SendMessage("lifeLoss", HP);

        HP--;

        //HP���c���Ă���
        if (HP > 0)
        {
            //�m�b�N�o�b�N
            rg.AddForce(new Vector2(-1 * Mathf.Sign(transform.localScale.x) * 200, 200));

            //�_���[�W���o�Ŕ�������
            sp.color = new Color(1f, 1f, 1f, 0.5f);

            yield return new WaitForSeconds(1);

            sp.color = new Color(1f, 1f, 1f, 1);

            isDamage = false;

            anim.SetBool("isDamage", isDamage);
        }

        //HP��0�ɂȂ�����
        else
        {
            rg.AddForce(new Vector2(0, 300));

            GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(0.5f);

            //�Q�[���I�[�o�[���o
            canvas.SendMessage("gameOverFadeOut");
        }
    }
}
