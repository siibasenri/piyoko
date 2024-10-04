using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxHeadScript : MonoBehaviour
{
    EdgeCollider2D Head;
    GameObject Fox;
    bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        Head = GetComponent<EdgeCollider2D>();
        Fox = transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            StartCoroutine("Dead");
        }
    }

    IEnumerator Dead()
    {
        transform.parent.gameObject.GetComponent<FoxScript>().isDead = true;
        Head.enabled = false;
        Fox.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        Fox.GetComponent<BoxCollider2D>().enabled = false;

        Fox.GetComponent<FoxScript>().isDead = true;

        Fox.GetComponent<AudioSource>().Play();

        Fox.GetComponent<FoxScript>().anim.SetBool("isDead", true);

        yield return new WaitForSeconds(1);

        Destroy(Fox);
    }
}
