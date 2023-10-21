using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    [field: SerializeField] public float movementSpeed { get; private set; } = 5f;
    [field: SerializeField] public float attackRadius { get; private set; } = 5f;
    [field: SerializeField] public float attackRange { get; private set; } = 1f;
    [field: SerializeField] public float attackDamage { get; private set; } = 5f;
    [field: SerializeField] public float attackDelay { get; private set; } = 1f;
    [field: SerializeField] public float maxHp { get; private set; } = 5f;

    private float currentHp;
    private void Start()
    {
        currentHp = maxHp;
    }

    public void GetHit(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }
    private void Die()
    {
        Debug.Log($"Enemy {name} has been killed!");
        Destroy(gameObject);
    }
}