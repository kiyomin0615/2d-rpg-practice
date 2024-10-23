using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform newGameButtonRectTransform;
    [SerializeField] GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreenUI;

    private string gameSceneName = "Game";
    

    void Start()
    {
        if (!SaveManager.instance.HasSaveData())
        {
            continueButton.SetActive(false);

            newGameButtonRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            newGameButtonRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            newGameButtonRectTransform.anchoredPosition = new Vector2(0f, -200f);
        }
    }

    public void StartNewGame()
    {
        SaveManager.instance.DeleteSaveData();

        StartCoroutine("LoadSceneWithFadeEffect", 2f);
    }

    public void ContinueGame()
    {
        StartCoroutine("LoadSceneWithFadeEffect", 2f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float duration)
    {
        fadeScreenUI.FadeOut();
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(gameSceneName);
    }
}
