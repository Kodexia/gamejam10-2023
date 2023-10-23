using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Cutscene");
        Time.timeScale = 1.0f;

    }
    public void LoadMenuScene()
    {

        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1.0f;

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void resumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    } 
}
