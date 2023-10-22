using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickPlaySFX : MonoBehaviour
{
    public static AudioSource instance;
    GameObject SFXobject;
    AudioSource SFX;

    void Awake()
    {
        
        SFX = GameObject.Find("guiButtons").GetComponent<AudioSource>(); ;
        if (instance != null)
            Destroy(SFXobject);
        else
        {
            instance = SFX;
            DontDestroyOnLoad(SFXobject);
        }
    }
    public void playSFX()
    {
        SFX.Play();
    }
   
}
