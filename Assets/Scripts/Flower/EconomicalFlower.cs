using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicalFlower : FlowerScript
{
    [SerializeField]
    private float boostSpeed = 2f;

    private WaterScript _selectedPond;
    private bool _isInitialized = false;

    protected override void FlowerStart()
    {
        flower.Priority = 2;
        InitFlower();
    }

    protected override void FlowerUpdate()
    {
        // Isn't needed right now
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
                continue;
            }
            
            if(pond.GetComponent<WaterScript>().isBoosted) continue;
            Vector3 flowerPosition = flower.flowerObject.transform.position;
            float distance = Vector3.Distance(pond.transform.position, flowerPosition);
            float closestDistanceDistance = Vector3.Distance(closestDistance.transform.position, flowerPosition);
            
            if (distance < closestDistanceDistance) closestDistance = pond;
        }

        if (closestDistance == null)
        {
            Debug.LogException(new Exception("No eligible pond found!"));
            return;
        }

        _isInitialized = true;
        _selectedPond = closestDistance.GetComponent<WaterScript>();
        _selectedPond.EcoFlowerBoost(boostSpeed);
    }
}
