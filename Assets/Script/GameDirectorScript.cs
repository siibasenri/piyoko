using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirectorScript : MonoBehaviour
{
    public GameObject[] reef,spark;
    GameObject Player;
    float reeftime,sparktime;
    int reefKind,sparkPos,sparkKind;

    void Start()
    {
        Player = GameObject.Find("Player");
    }


    void Update()
    {
        reeftime += Time.deltaTime;
        sparktime += Time.deltaTime;

        if (reeftime > 1f)
        {
            reefKind = Random.Range(0, 3);
            Instantiate(reef[reefKind], new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
            reeftime = 0;
        }

        if (sparktime > 3f)
        {
            sparkPos = Random.Range(-8, 8);
            sparkKind = Random.Range(0, 2);
            Instantiate(spark[sparkKind], new Vector3(Player.transform.position.x + sparkPos, -0.3f, 0.0f), Quaternion.identity);
            sparktime = 0;
        }
    }
}
