using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

//�Q�[���̐i�s���Ǘ�����N���X
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
     *              �eUI�̎擾�Ə���
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


    //�t�F�[�h�C��(�X�e�[�W�J�n��)�̉��o
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

    //�t�F�[�h�A�E�g(�Q�[���I�[�o�[��)�̉��o
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

    //�t�F�[�h�A�E�g(�Q�[���N���A��)�̉��o
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

    //�R�C���𓾂����̏���
    void scoreAdd(int count)
    {
        point += count;
        scoreText.text = point.ToString();
    }

    //���C�t�����������̉��o
    void lifeLoss(int count)
    {
        hearts[3 - count].GetComponent<Image>().enabled = false;
    }

    //�Q�[���I���̏���
    public void exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;    //�Q�[���v���C�I��
        #else
            Application.Quit();                                 //�Q�[���A�v���I��
        #endif
    }

    //���X�^�[�g�{�^���̏���
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //�l�N�X�g�X�e�[�W�{�^���̏���
    public void next()
    {
        PointData.point = point;                                //���p���p�Ƀ|�C���g���󂯓n��
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //�X�R�A�̈��p���p
    public static class PointData
    {
        public static int point = 0;
    }
}
