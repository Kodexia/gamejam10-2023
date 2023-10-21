using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMovement))]
public class CharacterBehaviour : MonoBehaviour
{
    CharacterMovement characterMovement;
    CharacterStats stats;
    MainFlowerScript mainFlower;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        stats = GetComponent<CharacterStats>();
        mainFlower = GameManager.instance.mainFlower;

        characterMovement.onEnemyTarget += (string n) => { TargetEnemy(n); };

        StartCoroutine(DamageOnOutsideZone());
    }
    private void Update()
    {
        HydrateFlowerIfCan();
    }
    void TargetEnemy(string name)
    {
        Debug.Log($"Targeted {name}");
    }

    void DamageAndHeal()
    {
        Flower flower = mainFlower.flower;

        if (!mainFlower.IsPlayerInRange(transform.position))
            stats.Hit(stats.outsideZoneDamage);
        else
            stats.Heal(stats.insideZoneHeal);
    }

    private void HydrateFlowerIfCan()
    {
        if (mainFlower.IsPlayerInRange(transform.position))
        {
            mainFlower.AddWater(stats.waterDrainSpeed);
            stats.DrainWater(stats.waterDrainSpeed);
        }
    }

    private IEnumerator DamageOnOutsideZone()
    {
        while (true)
        {
            DamageAndHeal();
            yield return new WaitForSeconds(stats.zoneDamageHealDelay);
        }
    }
}