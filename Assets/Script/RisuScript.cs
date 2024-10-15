using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���X�Ɋւ���N���X
public class RisuScript : MonoBehaviour
{
    CapsuleCollider2D body;
    Rigidbody2D rg;
    Animator anim;
    AudioSource se;
    SpriteRenderer sp;
    bool isDead,isWall,isEnemy;�@//���S����A�ǂ��O�ɂ��邩����A�G���O�ɂ��邩����
    float speed = 3f;

    public LayerMask groundLayer;
    Vector2 head1Pos,head2Pos; //�i�s�����ɏ�Q�������邩�̔���p


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

    //�ǂɂԂ���܂ō��E�Ɉړ�����
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

            //�ǂ��G���ڂ̑O�ɂ���Ȃ�i�s�����𔽓]
            if (isWall || isEnemy)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            }
        }
        //���S���A�_�ł���
        else
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            sp.color = new Color(1f, 1f, 1f, level);
        }
    }


    //�����蔻��
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

    //�|���ꂽ���̏���
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
