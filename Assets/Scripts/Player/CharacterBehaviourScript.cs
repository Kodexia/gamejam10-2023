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


    [field: SerializeField] Transform specialRPreview;
    [field: SerializeField] SpecialR specialR;
    float meleeCooldown = 0;
    float specialRCooldown = 0;
    bool isUsingSpecialR = false;
    void Start()
    {
        characterMovement = GetComponent<CharacterMovementScript>();
        stats = GetComponent<CharacterStatsScript>();
        mainFlower = GameManager.instance.mainFlower;
        animator = GetComponent<Animator>();

        meleeCooldown = stats.meleeDelay;
        specialRCooldown = stats.specialRDelay;


        //characterMovement.onEnemyTarget += (EnemyBehaviourScript en) => { TargetEnemy(en); };

        StartCoroutine(DamageOnOutsideZone());
    }
    private void Update()
    {
        UpdateCooldowns();
        HydrateFlowerIfCan();
        if (Input.GetMouseButtonDown(0) && !isUsingSpecialR)
        {
            if (meleeCooldown <= 0)
            {
                Vector3 dir = Vector3.Normalize((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position));

                characterMovement.StopMovement();
                animator.SetFloat("Horizontal", -dir.x);
                animator.SetFloat("Vertical", -dir.y);
                if (attackAudio != null)
                    attackAudio.time = 0.15f;
                attackAudio.Play();
                animator.SetTrigger("Attack");
                meleeCooldown = stats.meleeDelay;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && specialRCooldown <= 0)
            isUsingSpecialR = true;
        if (isUsingSpecialR)
            UseSpecialAttackR();

    }

    void UseSpecialAttackR()
    {
        if (!specialRPreview.gameObject.activeInHierarchy)
            specialRPreview.gameObject.SetActive(true);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;
        specialRPreview.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            specialR.enabled = true;
            specialRPreview.gameObject.SetActive(false);
            isUsingSpecialR = false;
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            specialRPreview.gameObject.SetActive(false);
            isUsingSpecialR = false;
        }
        specialRCooldown = stats.specialRDelay;
    }
    void UpdateCooldowns()
    {
        meleeCooldown -= Time.deltaTime;
        specialRCooldown -= Time.deltaTime;
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