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
    
    private void Load()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
