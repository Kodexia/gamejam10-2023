using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsScript))]
[RequireComponent(typeof(CharacterMovementScript))]
[RequireComponent(typeof(Animator))]
public class CharacterBehaviourScript : MonoBehaviour
{
    CharacterMovementScript characterMovement;
    CharacterStatsScript stats;
    MainFlowerScript mainFlower;
    EnemyBehaviourScript targetEnemy;
    Animator animator;
    [SerializeField]
    AudioSource attackAudio;
    private float attackDelay = 1f;
    float cooldown = 1.5f;
    void Start()
    {
        characterMovement = GetComponent<CharacterMovementScript>();
        stats = GetComponent<CharacterStatsScript>();
        mainFlower = GameManager.instance.mainFlower;
        animator = GetComponent<Animator>();

        cooldown = attackDelay;
        characterMovement.onEnemyTarget += (EnemyBehaviourScript en) => { TargetEnemy(en); };

        StartCoroutine(DamageOnOutsideZone());
    }
    private void Update()
    {
        cooldown -= Time.deltaTime;
        HydrateFlowerIfCan();
        if (Input.GetMouseButtonDown(0))
        {
            if (cooldown <= 0)
            {
                Vector3 dir = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position));

                characterMovement.StopMovement();
                animator.SetFloat("Horizontal", -dir.x);
                animator.SetFloat("Vertical", -dir.y);
                if (attackAudio != null)
                    attackAudio.time = 0.15f;
                attackAudio.Play();
                animator.SetTrigger("Attack");
                cooldown = attackDelay;
            }

        }
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
        if (mainFlower.IsPlayerInRangeToWater(transform.position))
        {
            if (stats.currentWaterLevel > stats.waterDrainSpeed)
            {
                mainFlower.AddWater(stats.waterDrainSpeed);
                stats.DrainWater(stats.waterDrainSpeed);
                // ToDo: Add animation or particles for hydrating the flower
            }
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