using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveFlower : MonoBehaviour
{
    [SerializeField]
    private float radius = 20f;
    
    [SerializeField]
    private GameObject flowerObject;
    
    [SerializeField]
    private FlowerType type;
    
    [SerializeField]
    private float maxHealth = 600f;
    
    public Flower Flower { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Flower = new Flower(flowerObject, FlowerType.Defense, radius, maxHealth);
        Flower.Priority = 3;
    }
}
