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

    private GameObject _chooseFlowerCanvas;

    private GrassGrowth _grassGrowth;
    private HealthBarScript _barScript;
    
    public FlowerMain flower;
    private float currentWaterLevel = 0f;


    void Start()
    {
        flower = new FlowerMain(gameObject, radius, priority: 1);
        _grassGrowth = GetComponentInChildren<GrassGrowth>();
        _barScript = GetComponent<HealthBarScript>();

        _chooseFlowerCanvas = GameManager.instance.chooseFlowerCanvas;
        _chooseFlowerCanvas.SetActive(false);
    }

    void Update()
    {
        _barScript.UpdateHealthbar(flower.Health, flower.MaxHealth);
    }

    public void TakeDamage(float dmg)
    {
            flower.TakeDamage(dmg);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameManager.instance.playerTag))
        {
            BudSpawnerManager.instance.SpawnNewFlowerBud(flowerBudPrefabDefensive);
            //EnemySpawnerManager.instance.ChangeSpawnRate(0.001f);

            //Debug.Log("Player has collided with the flower!");
        }
    }

    public void AddWater(float water)
    {
        currentWaterLevel += water;
        if (currentWaterLevel >= maxWaterLevel)
        {
            _chooseFlowerCanvas.SetActive(true); 
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
