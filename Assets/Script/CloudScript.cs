using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    GameObject Camera;
    float beforepos, afterpos;


    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");

        beforepos = Camera.transform.position.x;
        

    }

    // Update is called once per frame
    void Update()
    {

        afterpos = Camera.transform.position.x;
        float deltamove = (beforepos - afterpos) * 0.99f;
        Vector2 pos = new Vector2(transform.position.x - deltamove, transform.position.y);
        transform.position = pos;


        beforepos = afterpos;

    }
}
