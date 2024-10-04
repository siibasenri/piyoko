using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    GameObject Player;
    public bool auto;
    int endPos, moveTime;

    void Start()
    {
        Player = GameObject.Find("Player");
        endPos = 200;
        moveTime = 300;

        if(auto)
        {
            transform.DOMoveX(endPos, moveTime);
            
        }
    }

    void Update()
    {
        // ƒvƒŒƒCƒ„[’Ç]ˆ—
        if (!auto && Player.transform.position.x >= -1 && Player.transform.position.x <= 199)
        {
            transform.position = new Vector3(
                Player.transform.position.x + 1,
                transform.position.y,
                transform.position.z);
        }

    }

}
