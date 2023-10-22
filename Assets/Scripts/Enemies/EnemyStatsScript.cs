using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBarScript))]
public class EnemyStatsScript : MonoBehaviour
{
    [field: SerializeField] public float movementSpeed { get; private set; } = 5f;
    [field: SerializeField] public float attackRadius { get; private set; } = 5f;
    [field: SerializeField] public float attackRange { get; private set; } = 1f;
    [field: SerializeField] public float attackDamage { get; private set; } = 5f;
    [field: SerializeField] public float attackDelay { get; private set; } = 1f;
    [field: SerializeField] public float maxHp { get; private set; } = 5f;
    private float currentHp;
    private HealthBarScript barScript;

    private void Start()
    {
        currentHp = maxHp;
        barScript = GetComponent<HealthBarScript>();
    }
    private void Update()
    {
        barScript.UpdateHealthbar(currentHp, maxHp);
    }
    public bool GetHit(float damage) // returns bool - if the last hit killed the enemy
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Die()
    {
        Debug.Log($"Enemy {name} has been killed!");
        Destroy(gameObject);
    }

}