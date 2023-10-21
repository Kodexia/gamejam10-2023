using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsScript : MonoBehaviour
{
    [field: Header("Player Variables")]
    [field: SerializeField] public float movementSpeed { get; private set; } = 10f;
    [field: SerializeField] public float maxHealth { get; private set; } = 10f;
    [field: SerializeField] public float zoneDamageHealDelay { get; private set; } = 1f;
    [field: SerializeField] public float outsideZoneDamage { get; private set; } = 1f;
    [field: SerializeField] public float insideZoneHeal { get; private set; } = 0.25f;
    [field: SerializeField] public float maxWaterLevel { get; private set; } = 100f;
    [field: SerializeField] public float waterSuckSpeed { get; private set; } = 0.05f;
    [field: SerializeField] public float waterDrainSpeed { get; private set; } = 1f;
    public float currentHealth { get; private set; } = 10f;
    public float currentWaterLevel { get; private set; } = 0f;

    [field: Header("TMP Variables -> Move to GameManager in the future!")]
    [field: SerializeField] public LayerMask groundMask { get; private set; }
    [field: SerializeField] public string enemyTag { get; private set; } = "Enemy";

    private void Start()
    {
        // Sets starting HP
        currentHealth = maxHealth;
    }
    public void Hit(float hitDamage)
    {
        // Reduce HP and check, if player hasn't die
        currentHealth -= hitDamage;

        if (currentHealth <= 0)
            Die();
    }
    private void Die()
    {
        // Player dies -> TODO ending screen...
        Debug.Log("You died!");
        Destroy(gameObject);
    }

    public void Heal(float heal)
    {
        // Adds some HP, if player's over the maximum ammount of HP clamp the HP
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
    }

    public void AddWater(float water)
    {
        // If can add water -> adds water and clamps it just in case
        Debug.Log("Adding water");
        Debug.Log("Current water level: " + currentWaterLevel + ", Water to add: " + water);
        if (CanAddWater(water))
            currentWaterLevel = Mathf.Clamp(currentWaterLevel + water, 0, maxWaterLevel);
        Debug.Log("Current water level after adding: " + currentWaterLevel);
    }

    public void DrainWater(float water)
    {
        // Removes water and makes sure it's not removing below 0
        Debug.Log("Draining water");
        Debug.Log("Current water level: " + currentWaterLevel + ", Water to drain: " + water);
        if (currentWaterLevel >= water)
            currentWaterLevel -= water;
        Debug.Log("Current water level after draining: " + currentWaterLevel);
    }

    public bool CanAddWater(float water)
    {
        return ((maxWaterLevel - currentWaterLevel) >= water);
    }
}