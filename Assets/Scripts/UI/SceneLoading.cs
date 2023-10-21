using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainGameScene");        
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
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
