using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkScript : MonoBehaviour
{
    SpriteRenderer sp;
    float time,sin;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        sin = Mathf.Sin(Time.time * 10);
        sp.color = new Color(1f, 1f, 1f, sin*0.5f);

        if (time > 2)
        {
            Destroy(gameObject);
        }
    }
}
