using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//���̃X�e�[�W�ɐi�ރN���X
public class StartCanvasScript : MonoBehaviour
{
    //���݂̃V�[�����擾���A���̃V�[�������[�h
    public void GameStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
