using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//葉っぱに関するクラス
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

    //回転しながら落ちていき、画面外にでたら破壊
    void Update()
    {
        transform.Rotate(0f, 0f, random);

        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
