using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(HealthBarScript))]
public class MainFlowerScript : MonoBehaviour
{
    [SerializeField] private float radius = 20f;
    [SerializeField] private float waterRadius = 3f;
    [field: SerializeField] private float maxWaterLevel = 100f;
    [SerializeField] FlowerType type;
    GameObject flowerObject;

    [SerializeField] public CanvasGroup chooseFlowerCanvas;
    [SerializeField] public GameObject flowerBudPrefabOffensive;
    [SerializeField] public GameObject flowerBudPrefabDefensive;
    [SerializeField] public GameObject flowerBudPrefabEconomic;

    GrassGrowth grassGrowth;
    public FlowerMain flower;
    private float currentWaterLevel = 0f;
    HealthBarScript barScript;


    void Start()
    {
        flower = new FlowerMain(gameObject, radius, priority: 1);
        grassGrowth = GetComponentInChildren<GrassGrowth>();
        barScript = GetComponent<HealthBarScript>();
    }

    void Update()
    {
        testDmg();
        barScript.UpdateHealthbar(flower.Health, flower.MaxHealth);
    }

    void testDmg()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flower.TakeDamage(10);
            Debug.Log("Current health of flower: " + flower.Health);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameManager.instance.playerTag))
        {
            
            //BudSpawnerManager.instance.SpawnNewFlowerBud(flowerBudPrefabDefensive);
            //EnemySpawnerManager.instance.ChangeSpawnRate(0.001f);

            //Debug.Log("Player has collided with the flower!");
        }
    }

    public void AddWater(float water)
    {
        currentWaterLevel += water;
        if (currentWaterLevel >= maxWaterLevel)
        {
            chooseFlowerCanvas.alpha = 1;
            chooseFlowerCanvas.interactable = true;
            Time.timeScale = 0;
        }

             //TODO -> Change the parameter to some dynamic changing of the bud prefabs
    }


    //on water fill

    public void NewBud(GameObject flowerBudPrefab)
    {
        currentWaterLevel = 0;
        
        BudSpawnerManager.instance.SpawnNewFlowerBud(flowerBudPrefab);
    }
    
    public bool IsPlayerInRange(Vector3 playerPosition)
    {
        return (Vector3.Distance(transform.position, playerPosition) <= radius);
    }

    public bool IsPlayerInRangeToWater(Vector3 playerPosition)
    {
        return (Vector3.Distance(transform.position, playerPosition) <= waterRadius);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
