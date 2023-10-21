using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicalFlower : MonoBehaviour
{
    [SerializeField]
    private float radius = 20f;
    
    [SerializeField]
    private GameObject flowerObject;
    
    [SerializeField]
    private FlowerType type;
    
    [SerializeField]
    private float maxHealth = 80f;
    
    [SerializeField]
    private float boostSpeed = 2f;

    public Flower Flower { get; private set; }

    private WaterScript _selectedPond;
    private bool _isInitialized = false;
    
    void Start()
    {
        Flower = new Flower(flowerObject, FlowerType.Economic, radius, maxHealth);
        InitFlower();
    }
    
    // Choose closest pond to flower
    // Add speed to the pond restoration
    // Call once the flower has landed and is ready
    void InitFlower()
    {
        GameObject[] pondObjects = GameObject.FindGameObjectsWithTag("Pond");
        GameObject closestDistance = null;
        
        foreach (GameObject pond in pondObjects)
        {
            if (closestDistance == null)
            {
                closestDistance = pond;
            }
            else
            {
                Vector3 flowerPosition = flowerObject.transform.position;
                float distance = Vector3.Distance(pond.transform.position, flowerPosition);
                float closestDistanceDistance = Vector3.Distance(closestDistance.transform.position, flowerPosition);
                if (distance < closestDistanceDistance)
                {
                    closestDistance = pond;
                }
            }
        }

        if (closestDistance == null)
        {
            Debug.LogException(new Exception("No pond found!"));
            return;
        }

        _isInitialized = true;
        _selectedPond = closestDistance.GetComponent<WaterScript>();
        _selectedPond.EcoFlowerBoost(boostSpeed);
    }
}
