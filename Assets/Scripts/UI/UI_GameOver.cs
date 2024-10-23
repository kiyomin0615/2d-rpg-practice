using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    Scene currenctScene;

    void Start()
    {
        currenctScene = SceneManager.GetActiveScene();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(currenctScene.name);
    }
}
