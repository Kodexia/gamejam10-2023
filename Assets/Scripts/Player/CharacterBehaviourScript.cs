using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsScript))]
[RequireComponent(typeof(CharacterMovementScript))]
public class CharacterBehaviourScript : MonoBehaviour
{
    CharacterMovementScript characterMovement;
    CharacterStatsScript stats;
    MainFlowerScript mainFlower;
    EnemyBehaviourScript targetEnemy;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovementScript>();
        stats = GetComponent<CharacterStatsScript>();
        mainFlower = GameManager.instance.mainFlower;

        characterMovement.onEnemyTarget += (EnemyBehaviourScript en) => { TargetEnemy(en); };

        StartCoroutine(DamageOnOutsideZone());
    }
    private void Update()
    {
        HydrateFlowerIfCan();
    }
    void TargetEnemy(EnemyBehaviourScript en)
    {
        targetEnemy = en;
        Debug.Log($"Targeted {en.name}");
    }

    void DamageAndHeal()
    {
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
            //Debug.Log(mainFlower);
            DamageAndHeal();
            yield return new WaitForSeconds(stats.zoneDamageHealDelay);
        }
    }
}