using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OnClickPlaySFX : MonoBehaviour
{
    public static OnClickPlaySFX instance;

    void Awake()
    {
        if(this.gameObject.GetComponent<AudioSource>() != null)
        {
            if (instance != null)
                Destroy(gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
    public void playSFX()
    {
        Debug.Log("played sound");
        GameObject.Find("guiButtons").GetComponent<AudioSource>().Play();
    }
   
}
