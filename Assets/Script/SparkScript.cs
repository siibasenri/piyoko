using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//‰Î‰Ô‚ÉŠÖ‚·‚éƒNƒ‰ƒX
//—‹‚ª—Ž‚¿‚éˆÊ’u‚Ì—\‘ª‚Ì‚½‚ß‚ÉŒÄ‚Î‚ê‚é
public class SparkScript : MonoBehaviour
{
    SpriteRenderer sp;
    float time,sin;

    //Sprite‚ðŽæ“¾
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    //“_–Å‚µ‚ÄA2•bŒã‚É”j‰ó
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
