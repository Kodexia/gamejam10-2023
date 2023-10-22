using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuCanvas.active == false && chooseFlowerCanvas.active == false)
            {
                menuCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else if(menuCanvas.active == true)
            {
                menuCanvas.SetActive(false);
                Time.timeScale = 1;
            }
            
        }
    }
    [field: SerializeField] public MainFlowerScript mainFlower { get; private set; }
    [field: SerializeField] public CharacterBehaviourScript playerBehaviour { get; private set; }
    [field: SerializeField] public string playerTag { get; private set; } = "Player";
    [field: SerializeField] public string enemyTag { get; private set; } = "Enemy";
    [field: SerializeField] public string flowerTag { get; private set; } = "Flower";
    [field: SerializeField] public string allyTag { get; private set; } = "Ally";
    [field: SerializeField] public string pondTag { get; private set; } = "Pond";
    [field: SerializeField] public string mainFlowerTag { get; private set; } = "MainFlower";

    [field: SerializeField] public GameObject flowerBudPrefabOffensive { get; private set; }
    [field: SerializeField] public GameObject flowerBudPrefabDefensive { get; private set; }
    [field: SerializeField] public GameObject flowerBudPrefabEconomic { get; private set; }

    [field: SerializeField] public GameObject menuCanvas { get; private set; }
    [field: SerializeField] public GameObject chooseFlowerCanvas { get; private set; }
    public int numOfFlowers = 0;

    public void EndGame(bool win)
    {
        if (win)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    public void IncreaseFlowerCount()
    {
        numOfFlowers++;

        if (numOfFlowers >= 16)
        {
            EndGame(true);
        }
    }


}
