using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReefScript : MonoBehaviour
{
    Rigidbody2D rg;
    int random;
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
        rg = GetComponent<Rigidbody2D>();


        random = Random.Range(1, 5);
        transform.position = new Vector2(Player.transform.position.x - 5 - random, Player.transform.position.y + 5 + random);
        rg.AddForce(new Vector2(500 + (random * 100), 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, random);

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
