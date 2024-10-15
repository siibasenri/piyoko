using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//”wŒi‚ÌŽ÷–Ø‚ÉŠÖ‚·‚éƒNƒ‰ƒX
public class ForestScript : MonoBehaviour
{
    GameObject Player;
    // 
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // 
    void Update()
    {
        //‰æ–Ê”ÍˆÍ“à‚È‚çƒvƒŒƒCƒ„[‚ÌˆÊ’u‚É’Ç]‚·‚é
        if (Player.transform.position.x >= -1 && Player.transform.position.x <= 199)
        {

            transform.position = new Vector3(
                Player.transform.position.x + 1,
                transform.position.y,
                transform.position.z);
        }
    }
}
