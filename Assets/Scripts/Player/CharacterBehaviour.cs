using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMovement))]
public class CharacterBehaviour : MonoBehaviour
{
    CharacterMovement characterMovement;
    CharacterStats stats;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        stats = GetComponent<CharacterStats>();

        characterMovement.onEnemyTarget += (string n) => { TargetEnemy(n); };

        StartCoroutine(DamageOnOutsideZone());
    }
    void TargetEnemy(string name)
    {
        Debug.Log($"Targeted {name}");
    }

    void DamageAndHeal()
    {
        MainFlowerScript mainFlower = GameManager.instance.mainFlower;
        Flower flower = mainFlower.flower;
        float radius = flower.Radius;
        Vector3 mainFlowerPos = mainFlower.transform.position;
        float distanceFromFlower = Vector3.Distance(transform.position, mainFlowerPos);
        if (distanceFromFlower > radius)
            stats.Hit(stats.outsideZoneDamage);
        else
            stats.Heal(stats.insideZoneHeal);
    }

    private IEnumerator DamageOnOutsideZone()
    {
        while(true)
        {
            DamageAndHeal();
            yield return new WaitForSeconds(stats.zoneDamageHealDelay);
        }
    }
}