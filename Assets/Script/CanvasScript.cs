using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

//ゲームの進行を管理するクラス
public class CanvasScript : MonoBehaviour
{
    public GameObject[] hearts;
    GameObject score, gameOver, gameClear, fade, restartButton, exitButton, nextStageButton;
    RectTransform faderectTransform;
    Text scoreText, gameOverText, gameClearText;
    int point;

    AudioSource se;

    [SerializeField] AudioClip ClearBGM;
    [SerializeField] AudioClip GameOverBGM;

    /***********************************************
     * 
     *              各UIの取得と処理
     * 
     * ********************************************/
    void Start()
    {
        score = GameObject.Find("ScoreText");
        scoreText = score.GetComponent<Text>();
        scoreText.text = PointData.point.ToString();

        gameOver = GameObject.Find("GameOver");
        gameOverText = gameOver.GetComponent<Text>();

        gameClear = GameObject.Find("GameClear");
        gameClearText = gameClear.GetComponent<Text>();


        restartButton = GameObject.Find("Restart");
        exitButton = GameObject.Find("Exit");
        nextStageButton = GameObject.Find("NextStage");

        fade = GameObject.Find("Fade");
        faderectTransform = fade.GetComponent<RectTransform>();

        se = GetComponent<AudioSource>();

        point = 0;
        fadeIn();
    }


    //フェードイン(ステージ開始時)の演出
    void fadeIn()
    {
        restartButton.SetActive(false);
        exitButton.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex != 3)
            nextStageButton.SetActive(false);

        gameOverText.enabled = false;
        gameClearText.enabled = false;

        faderectTransform.DOScale(new Vector3(1, 0, 1), 1.5f).SetEase(Ease.InOutQuint).SetLink(fade);
    }

    //フェードアウト(ゲームオーバー時)の演出
    void gameOverFadeOut()
    {
        gameOverText.enabled = true;
        faderectTransform.DOScale(new Vector3(1, 1, 1), 1.5f).SetEase(Ease.InOutQuint).SetLink(fade);

        restartButton.SetActive(true);
        exitButton.SetActive(true);

        se.Stop();
        se.PlayOneShot(GameOverBGM);
        Debug.Log("gameover");
    }

    //フェードアウト(ゲームクリア時)の演出
    void gameClearFadeOut()
    {
        gameClearText.enabled = true;
        faderectTransform.DOScale(new Vector3(1, 1, 1), 1.5f).SetEase(Ease.InOutQuint).SetLink(fade);

        restartButton.SetActive(true);
        exitButton.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex != 3)
            nextStageButton.SetActive(true);

        se.Stop();
        se.PlayOneShot(ClearBGM);
    }

    //コインを得た時の処理
    void scoreAdd(int count)
    {
        point += count;
        scoreText.text = point.ToString();
    }

    //ライフを失った時の演出
    void lifeLoss(int count)
    {
        hearts[3 - count].GetComponent<Image>().enabled = false;
    }

    //ゲーム終了の処理
    public void exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;    //ゲームプレイ終了
        #else
            Application.Quit();                                 //ゲームアプリ終了
        #endif
    }

    //リスタートボタンの処理
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //ネクストステージボタンの処理
    public void next()
    {
        PointData.point = point;                                //引継ぎ用にポイントを受け渡す
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //スコアの引継ぎ用
    public static class PointData
    {
        public static int point = 0;
    }
}
