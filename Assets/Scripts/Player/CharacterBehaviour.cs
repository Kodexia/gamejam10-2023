using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMovement))]
public class CharacterBehaviour : MonoBehaviour
{

    CharacterMovement characterMovement;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterMovement.onEnemyTarget += (string n) => { TargetEnemy(n); };
    }

    void Update()
    {
        
    }

    void TargetEnemy(string name)
    {
        Debug.Log($"Targeted {name}");
    }
}
