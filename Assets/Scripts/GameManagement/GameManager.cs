using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    [field: SerializeField] public MainFlowerScript mainFlower { get; private set; }
    [field: SerializeField] public CharacterBehaviourScript playerBehaviour { get; private set; }
    [field: SerializeField] public string playerTag { get; private set; } = "Player";
    [field: SerializeField] public string enemyTag { get; private set; } = "Enemy";
    [field: SerializeField] public string flowerTag { get; private set; } = "Flower";
}
