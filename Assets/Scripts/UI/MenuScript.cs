using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
   public void LoadGameScene()
   {
        SceneManager.LoadScene("MainGameScene");        
   }
   public void ExitGame()
   {
        Application.Quit();
   }
}
