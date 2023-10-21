using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject mainFlowerPrefab;
    [SerializeField] GameObject flowerBudPrefab;

    public static SpawnerManager instance;

    private float minRadius;
    private float maxRadius;
    public float budDiameter;
    private List<Vector2> spawnedBuds;
    private float currentAngle = 0f;


    private float middleRadius;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        spawnedBuds = new List<Vector2>();


        SpriteRenderer budSpriteRenderer = flowerBudPrefab.GetComponentInChildren<SpriteRenderer>();
        if (budSpriteRenderer != null)
        {
            budDiameter = budSpriteRenderer.sprite.bounds.size.x * flowerBudPrefab.transform.lossyScale.x;
        }
        else
        {
            Debug.LogError("flowerBudPrefab does not have a spriteRenderer");
        }


        SpriteRenderer mainFlowerSpriteRenderer = mainFlowerPrefab.GetComponentInChildren<SpriteRenderer>();
        if (mainFlowerSpriteRenderer != null)
        {
            float mainFlowerDiameter = mainFlowerSpriteRenderer.sprite.bounds.size.x * mainFlowerPrefab.transform.lossyScale.x;

            minRadius = (mainFlowerDiameter / 2) + budDiameter;
            maxRadius = minRadius;
        }
        else
        {
            Debug.LogError("MainFlowerPrefab does not have a spriteRenderer");
        }

        middleRadius = (minRadius + maxRadius) / 2f;
    }


    //void Update()
    //{
    //    SpawnNewFlowerBud(flowerBudPrefabEconomic);

    //}

    public void SpawnNewFlowerBud(GameObject flowerBudPrefab)
    {
        Vector2 spawnPosition = GetNewPosition();

        GameObject newBudObject = Instantiate(flowerBudPrefab, spawnPosition, Quaternion.identity);
        spawnedBuds.Add(spawnPosition);
    }

    public Vector2 GetNewPosition() // returns verctor2 available position
    {
        Vector2 spawnPosition = Vector2.zero;
        bool canSpawnHere = false;
        int safetyNet = 0;

        float goldenAngle = 137.5f * Mathf.Deg2Rad;

        while (!canSpawnHere)
        {
            if (safetyNet > 100)
            {
                Debug.LogError("Too many attempts to spawn, space might be full!");

                minRadius += 1;
                maxRadius += 1;
                middleRadius = (minRadius + maxRadius) / 2f;
                safetyNet = 0;
            }

            currentAngle += goldenAngle;
            if (currentAngle > 2 * Mathf.PI)
            {
                currentAngle -= 2 * Mathf.PI;
            }

            spawnPosition = new Vector2(middleRadius * Mathf.Cos(currentAngle), middleRadius * Mathf.Sin(currentAngle));

            canSpawnHere = CheckIfCanSpawn(spawnPosition);
            safetyNet++;
        }

        return spawnPosition;

        bool CheckIfCanSpawn(Vector2 position)
        {
            foreach (Vector2 bud in spawnedBuds)
            {

                if (Vector2.Distance(position, bud) < budDiameter)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
