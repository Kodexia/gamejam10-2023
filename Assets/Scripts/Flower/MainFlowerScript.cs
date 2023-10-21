using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(HealthBarScript))]
public class MainFlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [field: SerializeField] private float maxWaterLevel = 100f;
    [SerializeField] FlowerType type;
    GameObject flowerObject;

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
            BudSpawnerManager.instance.SpawnNewFlowerBud(flowerBudPrefabDefensive);

            Debug.Log("Player has collided with the flower!");
        }
    }

    public void AddWater(float water)
    {
        currentWaterLevel += water;
        if (currentWaterLevel >= maxWaterLevel)
            NewBud(flowerBudPrefabDefensive); //TODO -> Change the parameter to some dynamic changing of the bud prefabs
    }


    //on water fill

    public void NewBud(GameObject flowerBudPrefab)
    {
        currentWaterLevel = 0;

        flower.ShootOutBud(new Vector2(1, 1), flowerBudPrefab);
        Debug.Log("Shot new bud!");
    }
    
    public bool IsPlayerInRange(Vector3 playerPosition)
    {
        return (Vector3.Distance(transform.position, playerPosition) <= radius);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
