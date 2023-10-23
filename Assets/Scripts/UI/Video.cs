using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    [SerializeField] float delay = 37;
    void Start()
    {
        Invoke("Load", delay);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Load();
        }
    }
    
    private void Load()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
