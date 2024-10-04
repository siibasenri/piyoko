using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScript : MonoBehaviour
{
    SpriteRenderer sp;
    float sin,time,dis;
    Collider2D col;
    AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        se = GetComponent<AudioSource>();
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        se.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        sin = Mathf.Sin(Time.time*10);
        sp.color = new Color(1f, 1f, 1f, sin);

        if (time > 2)
        {
            Destroy(gameObject);
        }
    }
}
