using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//飛行機のプロペラに関するクラス
public class propellerScript : MonoBehaviour
{
    public int rotateFocus;


    /*****************************
     * 
     *    プロペラの回転処理
     * 
     * *************************/
    void Update()
    {
        transform.Rotate(0f, 0f, rotateFocus * 0.1f);
    }
}