using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propellerScript : MonoBehaviour
{
    public int rotateFocus;


    /*****************************
     * 
     *    ƒvƒƒyƒ‰‚Ì‰ñ“]ˆ—
     * 
     * *************************/
    void Update()
    {
        transform.Rotate(0f, 0f, rotateFocus * 0.1f);
    }
}