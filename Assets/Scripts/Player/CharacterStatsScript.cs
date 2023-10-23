using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    [field: SerializeField] public float attackDamage { get; private set; } = 2.5f;
    public float currentHealth { get; private set; } = 10f;
    public float currentWaterLevel { get; private set; } = 0f;

    [field: Header("TMP Variables -> Move to GameManager in the future!")]
    [field: SerializeField] public LayerMask groundMask { get; private set; }
    [field: SerializeField] public string enemyTag { get; private set; } = "Enemy";

    [Header("add WaterLeft object in WaterBar prefab")]
    [SerializeField] GameObject waterBar;
    [SerializeField] GameObject oxygenBar;
    [SerializeField] Gradient oxygenBarGradient;
    [SerializeField] AudioSource waterDrainAudio;

    private void Start()
    {
        // Sets starting HP
        currentHealth = maxHealth;
        updateWaterBar();

    }
    public void Hit(float hitDamage)
    {
        // Reduce HP and check, if player hasn't die
        currentHealth -= hitDamage;
        UpdateOxygenBar();
        if (currentHealth <= 0)
            Die();
    }
    private void Die()
    {
        SceneManager.LoadScene("SuffocatedScene");
        Debug.Log("You died!");
        Destroy(gameObject);
    }

    public void Heal(float heal)
    {
        // Adds some HP, if player's over the maximum ammount of HP clamp the HP
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        UpdateOxygenBar();
    }

    public void AddWater(float water)
    {
        // If can add water -> adds water and clamps it just in case
        if (!CanAddWater(water)) { waterDrainAudio.Stop(); }
        if (CanAddWater(water))
            if (waterDrainAudio.isPlaying == false) waterDrainAudio.Play();
            currentWaterLevel = Mathf.Clamp(currentWaterLevel + water, 0, maxWaterLevel);
        
        updateWaterBar();
    }

    public void DrainWater(float water)
    {
        // Removes water and makes sure it's not removing below 0
        if (currentWaterLevel >= water)
            currentWaterLevel -= water;
        
        updateWaterBar();
    }

    public bool CanAddWater(float water)
    {
        return ((maxWaterLevel - currentWaterLevel) >= water);
    }
    public void updateWaterBar()
    {
        waterBar.GetComponent<SpriteRenderer>().size = new Vector2(currentWaterLevel/maxWaterLevel*12,2);
    }
    public void UpdateOxygenBar()
    {
        float o2 = currentHealth / maxHealth;
        SpriteRenderer r = oxygenBar.GetComponent<SpriteRenderer>();
        r.color = oxygenBarGradient.Evaluate(o2);
        r.size = new Vector2(o2*12,2);
    }
}