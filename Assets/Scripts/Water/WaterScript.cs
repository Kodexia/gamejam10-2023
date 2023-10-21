using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;
using System.Linq;
using UnityEngine.Serialization;

public class WaterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pondObject;

    [SerializeField]
    private float maximumWaterCapacity = 200;

    [SerializeField]
    private List<Sprite> waterSprites;
    [field: SerializeField] private float playerDistanceToAdd { get; set; } = 1f;
    [field: SerializeField] private float waterRegenSpeed { get; set; } = 0.05f;

    private bool _isTransitioning = false;
    private float _currentWaterCapacity = 0;
    private int[] _stages;
    private int _currentStage = 0;
    private CharacterStatsScript _playerStats;
    
    private SpriteRenderer _bgWaterSpriteRenderer;
    private SpriteRenderer _waterSpriteRenderer;
    private GameObject _bgWaterGameObject;

    public bool isBoosted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentWaterCapacity = maximumWaterCapacity;

        _stages = new int[waterSprites.Count];
        for (int i = 1; i <= waterSprites.Count; i++)
        {
            _stages[i - 1] = (int)maximumWaterCapacity / i;
        }
        
        _playerStats = GameManager.instance.playerBehaviour.GetComponent<CharacterStatsScript>();
        
        if (_waterSpriteRenderer == null)
        {
            _waterSpriteRenderer = pondObject.GetComponent<SpriteRenderer>();

            if (_waterSpriteRenderer == null)
            {
                _waterSpriteRenderer = pondObject.AddComponent<SpriteRenderer>();
            }
        }
        
        _bgWaterGameObject = new GameObject("BG_WaterSprite");
        _bgWaterGameObject.transform.SetParent(pondObject.transform, false);
        _bgWaterGameObject.transform.localPosition = Vector3.zero;
        
        _bgWaterSpriteRenderer = _bgWaterGameObject.AddComponent<SpriteRenderer>();
        _bgWaterSpriteRenderer.sortingOrder = _waterSpriteRenderer.sortingOrder - 1;  // Ensure it's behind the foreground sprite
        _bgWaterSpriteRenderer.sprite = waterSprites[_currentStage];

        ChangeSprite(_currentStage);
    }

    public void EcoFlowerBoost(float boost)
    {
        if (!isBoosted)
        {
            waterRegenSpeed *= boost;
            isBoosted = true;
            return;
        }
        
        Debug.LogException(new Exception("EcoFlowerBoost called twice!"));
    }

    // Update is called once per frame
    void Update()
    {
        // Add check for if the player is nearby and is charging water
        float playerDistanceFromPond = Vector3.Distance(transform.position, _playerStats.transform.position);
        if (playerDistanceFromPond <= playerDistanceToAdd)
            ChargeWater(_playerStats.waterSuckSpeed);
        else
            AddWater(waterRegenSpeed);
    }

    // Called when the water is being charged
    void ChargeWater(float amount)
    {
        if (!_playerStats.CanAddWater(amount) || !(_currentWaterCapacity >= amount)) return;
        
        _playerStats.AddWater(amount);
        _currentWaterCapacity -= amount;
        _currentWaterCapacity = Mathf.Max(_currentWaterCapacity, 0);
        ChangeSpriteIfNeeded(Action.Charged);
    }

    // Called when the water is being added
    void AddWater(float amount)
    {
        _currentWaterCapacity = Mathf.Clamp(_currentWaterCapacity + amount, 0, maximumWaterCapacity);

        ChangeSpriteIfNeeded(Action.Added);
    }

    int CalculateStageWhenCharged()
    {
        if (_currentWaterCapacity == maximumWaterCapacity)
        {
            return 0;
        }

        int newSpriteIndex = 0;
        for (int i = 0; i < _stages.Count(); i++)
        {
            if (_currentWaterCapacity <= _stages[i])
            {
                newSpriteIndex = i;
            }
        }

        if (newSpriteIndex == _stages.Count() - 1)
        {
            if (_currentWaterCapacity > 3)
            {
                newSpriteIndex = _stages.Count() - 2;
            }
        }

        return newSpriteIndex;
    }
    
    IEnumerator SpriteTransition(int newStage)
    {
        _isTransitioning = true;

        _bgWaterSpriteRenderer.sprite = waterSprites[newStage];
        SetSpriteAlpha(_bgWaterSpriteRenderer, 1f);  // Reset alpha of background sprite

        // Fade out the foreground sprite to reveal the background sprite
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            SetSpriteAlpha(_waterSpriteRenderer, alpha);
            yield return new WaitForSeconds(0.05f);
        }

        // Swap the sprites, making the previous background sprite the new foreground sprite
        _waterSpriteRenderer.sprite = _bgWaterSpriteRenderer.sprite;
        SetSpriteAlpha(_waterSpriteRenderer, 1f);  // Reset alpha of foreground sprite
        SetSpriteAlpha(_bgWaterSpriteRenderer, 0f);  // Hide the background sprite

        _currentStage = newStage;
        _isTransitioning = false;
    }
    
    void SetSpriteAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color currentColor = spriteRenderer.color;
        currentColor.a = alpha;
        spriteRenderer.color = currentColor;
    }
    
    int CalculateStageWhenAdded()
    {
        if (_currentWaterCapacity == maximumWaterCapacity)
        {
            return 0;
        }

        int newSpriteIndex = _stages.Count() - 1;
        for (int i = _stages.Count() - 1; i >= 0; i--)
        {
            if (_currentWaterCapacity >= _stages[i])
            {
                newSpriteIndex = i;
            }
        }

        if (newSpriteIndex == _stages.Count() - 1)
        {
            if (_currentWaterCapacity > 3)
            {
                newSpriteIndex = _stages.Count() - 2;
            }
        }

        return newSpriteIndex;
    }

    void ChangeSpriteIfNeeded(Action action)
    {
        int newStage = action == Action.Charged ? CalculateStageWhenCharged() : CalculateStageWhenAdded();
        if (_currentStage != newStage && ! _isTransitioning)
        {
            StartCoroutine(SpriteTransition(newStage));
        }
    }

    // Called when the sprite needs to be changed
    void ChangeSprite(int stage)
    {
        if (stage < 0 || stage >= waterSprites.Count)
        {
            Debug.LogWarning("Invalid stage provided to ChangeSprite.");
            return;
        }

        _waterSpriteRenderer.sprite = waterSprites[stage];
        _currentStage = stage;
    }
}

enum Action
{
    Charged,
    Added
}
