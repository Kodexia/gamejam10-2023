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
    [field: SerializeField] Material grassShaderMaterial;

    private GameObject _chooseFlowerCanvas;

    private GrassGrowth _grassGrowth;
    private HealthBarScript _barScript;
    
    public FlowerMain flower;
    private float currentWaterLevel = 0f;
    AudioSource deathMainAudio;
    [SerializeField]
    AudioSource addWaterFromPlayerSound;
    
    void Start()
    {
        flower = new FlowerMain(gameObject, radius, hp: 100, priority: 1);
        _grassGrowth = GetComponentInChildren<GrassGrowth>();
        _barScript = GetComponent<HealthBarScript>();
        deathMainAudio = this.gameObject.GetComponent<AudioSource>();

        _chooseFlowerCanvas = GameManager.instance.chooseFlowerCanvas;
        _chooseFlowerCanvas.SetActive(false);
    }

    void Update()
    {
        _barScript.UpdateHealthbar(flower.Health, flower.MaxHealth);
        Debug.Log($"Main flower: {flower.Health}HP");
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        grassShaderMaterial.SetFloat("Strength", (flower.Health/flower.MaxHealth)/2);
        grassShaderMaterial.SetFloat("_Strength", (flower.Health/flower.MaxHealth)/2);
    }

    public void TakeDamage(float dmg)
    {
        if (flower.TakeDamage(dmg))
        {
            deathMainAudio.Play();

            GameManager.instance.EndGame(false );
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameManager.instance.playerTag))
        {
            BudSpawnerManager.instance.SpawnNewFlowerBud(GameManager.instance.flowerBudPrefabDefensive);
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
        bool isMainFlowerInRange = (Vector3.Distance(transform.position, playerPosition) <= radius);
        if (!isMainFlowerInRange)
        {
            GameObject[] tagged = GameObject.FindGameObjectsWithTag(GameManager.instance.flowerTag);
            foreach (GameObject ob in tagged)
            {
                if (Vector3.Distance(playerPosition, ob.transform.position) <= (2 * radius / 3))
                    return true;
                else
                    continue;
            }
            return false;
        }
        else
            return isMainFlowerInRange;
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
