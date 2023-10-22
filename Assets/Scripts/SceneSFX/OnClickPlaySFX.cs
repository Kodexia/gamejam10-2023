using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickPlaySFX : MonoBehaviour
{
    public static AudioSource instance;
    [SerializeField]
    AudioSource SFX;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = SFX;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void playSFX()
    {
        SFX.Play();
    }
   
}
