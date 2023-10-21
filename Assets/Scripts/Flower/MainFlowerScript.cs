using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MainFlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 5f;
    [field: SerializeField] private float maxWaterLevel = 100f;
    [SerializeField] FlowerType type;
    GameObject flowerObject;

    [SerializeField] public GameObject flowerBudPrefabOffensive;
    [SerializeField] public GameObject flowerBudPrefabDefensive;
    [SerializeField] public GameObject flowerBudPrefabEconomic;

    public FlowerMain flower;
    private float currentWaterLevel = 0f;

    public MainFlowerScript()
    {
        flower = new FlowerMain(flowerObject, radius);
    }


    void Start()
    {
        flowerObject = gameObject;
        Debug.Log(flower.Health);
    }

    void Update()
    {
        testDmg();
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
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnerManager.instance.SpawnNewFlowerBud(flowerBudPrefabEconomic);

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
        return (Vector3.Distance(transform.position, playerPosition) <= flower.Radius);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
