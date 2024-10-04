using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgraund : MonoBehaviour
{
    GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2(Camera.transform.position.x,Camera.transform.position.y);
        transform.position = pos;
    }
}
