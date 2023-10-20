using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [field: SerializeField] public float movementSpeed { get; private set; } = 10f;
    [field: SerializeField] public LayerMask groundMask { get; private set; }
    [field: SerializeField] public string groundTag { get; private set; } = "Ground";
}