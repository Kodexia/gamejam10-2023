using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [field: Header("Player Variables")]
    [field: SerializeField] public float movementSpeed { get; private set; } = 10f;
    [field: SerializeField] public float maxHealth { get; private set; } = 10f;
    [field: SerializeField] public float zoneDamageHealDelay { get; private set; } = 1f;
    [field: SerializeField] public float outsideZoneDamage { get; private set; } = 1f;
    [field: SerializeField] public float insideZoneHeal { get; private set; } = 0.25f;
    public float currentHealth { get; private set; } = 10f;

    [field: Header("TMP Variables -> Move to GameManager in the future!")]
    [field: SerializeField] public LayerMask groundMask { get; private set; }
    [field: SerializeField] public string enemyTag { get; private set; } = "Enemy";

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void Hit(float hitDamage)
    {
        currentHealth -= hitDamage;

        if (currentHealth <= 0)
            Die();
    }
    private void Die()
    {
        Debug.Log("You died!");
        Destroy(gameObject);
    }
    
    public void Heal(float heal)
    {
        currentHealth += heal;

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }
}