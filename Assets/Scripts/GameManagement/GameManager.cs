using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    [field: SerializeField] public MainFlowerScript mainFlower { get; private set; }
}
