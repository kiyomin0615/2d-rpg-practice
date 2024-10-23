using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform newGameButtonRectTransform;
    [SerializeField] GameObject continueButton;

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

        SceneManager.LoadScene(gameSceneName);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
