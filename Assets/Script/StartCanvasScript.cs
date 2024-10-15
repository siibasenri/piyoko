using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//次のステージに進むクラス
public class StartCanvasScript : MonoBehaviour
{
    //現在のシーンを取得し、次のシーンをロード
    public void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
