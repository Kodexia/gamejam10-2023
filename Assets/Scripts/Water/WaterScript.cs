using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject waterObject;

    [SerializeField]
    private float maximumWaterCapacity = 200;
    
    [SerializeField]
    private List<Sprite> waterSprites;

    private float _currentWaterCapacity = 0;
    private int[] _stages;
    private int currentStage = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentWaterCapacity = maximumWaterCapacity;
        
        _stages = new int[waterSprites.Count];
        for (int i = 1; i <= waterSprites.Count; i++)
        {
            _stages[i - 1] = (int)maximumWaterCapacity / i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Add check for if the player is nearby and is charging water
    }
    
    // Called when the water is being charged
    void ChargeWater(float amount)
    {
        _currentWaterCapacity = Mathf.Clamp(_currentWaterCapacity + amount, 0, maximumWaterCapacity);
        
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
        for(int i = 0; i < _stages.Length; i++)
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
