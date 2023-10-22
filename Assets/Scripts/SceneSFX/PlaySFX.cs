using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField]
    AudioSource SFX;
    // Start is called before the first frame update
    void Start()
    {
        SFX.Play();
    }


}
