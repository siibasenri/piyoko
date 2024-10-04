using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderAreaScript : MonoBehaviour
{
    bool isIntoPlayer,isLight;
    public GameObject[] Thunder;
    GameObject lightning;
    Collider2D col;
    float wide,fallTime,fallPoint;
    // Start is called before the first frame update
    void Start()
    {
        fallTime = 0;
        col = GetComponent<Collider2D>();
        isIntoPlayer = false;
        isLight = false;
        wide = col.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIntoPlayer)
        {
            if(fallTime < 1 )
            {
                fallPoint = Random.Range(transform.position.x - wide * 0.5f, transform.position.x + wide * 0.5f);
            }
            if (fallTime > 2 && fallTime < 4)
            {
                if (!isLight)
                {
                    lightning = Instantiate(Thunder[0], new Vector3(fallPoint, 0.5f, 0.0f), Quaternion.identity);
                    isLight = true;
                }
                if (isLight)
                {
                    float sin = Mathf.Sin(fallTime * 10);
                    lightning.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, sin * 0.5f);
                }
            }
            if (fallTime > 4)
            {
                Destroy(lightning);
                Instantiate(Thunder[1], new Vector3(fallPoint, 0.5f, 0.0f), Quaternion.identity);
                isLight = false;
                fallTime = 0;
            }
            fallTime += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isIntoPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isIntoPlayer = false;
    }
}
