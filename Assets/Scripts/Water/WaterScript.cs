using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

public class WaterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject waterObject;

    [SerializeField]
    private float maximumWaterCapacity = 200;

    [SerializeField]
    private List<Sprite> waterSprites;
    [field: SerializeField] private float playerDistanceToAdd { get; set; } = 1f;
    [field: SerializeField] private float waterRegenSpeed { get; set; } = 0.05f;

    private float _currentWaterCapacity = 0;
    private int[] _stages;
    private int currentStage = 0;
    private CharacterStats playerStats;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentWaterCapacity = maximumWaterCapacity;

        _stages = new int[waterSprites.Count];
        for (int i = 1; i <= waterSprites.Count; i++)
        {
            _stages[i - 1] = (int)maximumWaterCapacity / i;
        }
        playerStats = GameManager.instance.playerBehaviour.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // Add check for if the player is nearby and is charging water
        float playerDistanceFromPond = Vector3.Distance(transform.position, playerStats.transform.position);
        if (playerDistanceFromPond <= playerDistanceToAdd)
            ChargeWater(playerStats.waterSuckSpeed);
        else
            AddWater(waterRegenSpeed);
    }

    // Called when the water is being charged
    void ChargeWater(float amount)
    {
        if (playerStats.CanAddWater(amount))
        {
            if (_currentWaterCapacity >= amount)
            {
                playerStats.AddWater(amount);
                // _currentWaterCapacity = Mathf.Clamp(_currentWaterCapacity - amount, 0, maximumWaterCapacity); - NOT WORKING!
                _currentWaterCapacity -= amount;
                if (_currentWaterCapacity < 0)
                    _currentWaterCapacity = 0;
            }
        }
        
        ChangeSpriteIfNeeded();
    }

    // Called when the water is being added
    void AddWater(float amount)
    {
        _currentWaterCapacity = Mathf.Clamp(_currentWaterCapacity + amount, 0, maximumWaterCapacity);

        ChangeSpriteIfNeeded();
    }

    int CalculateStage()
    {
        if (_currentWaterCapacity == maximumWaterCapacity)
        {
            return _stages.Length - 1;
        }

        int newSpriteIndex = 0;
        for (int i = 0; i < _stages.Length; i++)
        {
            if (_currentWaterCapacity <= _stages[i])
            {
                newSpriteIndex = i;
                break;
            }
        }

        return newSpriteIndex;
    }

    void ChangeSpriteIfNeeded()
    {
        int newStage = CalculateStage();
        if (currentStage != newStage)
        {
            ChangeSprite(newStage);
        }
    }

    // Called when the sprite needs to be changed
    void ChangeSprite(int stage)
    {
        // code for changing the sprite
    }
}
