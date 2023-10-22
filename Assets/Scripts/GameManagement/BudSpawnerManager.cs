using System.Collections.Generic;
using UnityEngine;

public class BudSpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject mainFlowerPrefab;
    [SerializeField] GameObject flowerBudPrefab;
    [SerializeField] private GameObject seedPrefab;

    public static BudSpawnerManager instance;

    private float minRadius;
    private float maxRadius;
    [SerializeField] float budDiameter = 4.5f;
    [SerializeField] float mainFlowerDiameter = 3f;
    [SerializeField] int maxNubOfLoops = 10;
    private List<Vector2> spawnedBuds;
    private float currentAngle = 0f;
    public int numOfLoops { get; private set; }


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


        //SpriteRenderer budSpriteRenderer = flowerBudPrefab.GetComponentInChildren<SpriteRenderer>();
        //if (budSpriteRenderer != null)
        //{
        //    budDiameter = budSpriteRenderer.sprite.bounds.size.x * flowerBudPrefab.transform.lossyScale.x;
        //}
        //else
        //{
        //    Debug.LogError("flowerBudPrefab does not have a spriteRenderer");
        //}


        SpriteRenderer mainFlowerSpriteRenderer = mainFlowerPrefab.GetComponentInChildren<SpriteRenderer>();
        if (mainFlowerSpriteRenderer != null)
        {
            //mainFlowerDiameter = mainFlowerSpriteRenderer.sprite.bounds.size.x * mainFlowerPrefab.transform.lossyScale.x;

            minRadius = (mainFlowerDiameter / 2) + budDiameter;
            maxRadius = minRadius;
        }
        else
        {
            Debug.LogError("MainFlowerPrefab does not have a spriteRenderer");
        }

        middleRadius = (minRadius + maxRadius) / 2f;


        // add ponds to list of spawned buds

        List<GameObject> ponds = this.GetNearbyPonds();
        Debug.Log("ponds: " +  ponds.Count);
        foreach (GameObject pond in ponds)
        {
            spawnedBuds.Add(pond.transform.position);
        }
    }


    //void Update()
    //{
    //    SpawnNewFlowerBud(flowerBudPrefabEconomic);

    //}

    public void SpawnNewFlowerBud(GameObject flowerBudPrefab)
    {
        Vector2 endPosition = GetNewPosition();

        MainFlowerScript script = GameManager.instance.mainFlower;
        Vector2 startPosition = script.flower.flowerObject.transform.position;
        
        BudMoveInArc budMoveInArc = script.flower.flowerObject.GetComponent<BudMoveInArc>();
        budMoveInArc.ShootOutBud(startPosition, endPosition, seedPrefab, flowerBudPrefab);

        GameManager.instance.IncreaseFlowerCount();

        spawnedBuds.Add(endPosition);
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
                //Debug.LogError("Too many attempts to spawn, space might be full!");

                minRadius += 1;
                maxRadius += 1;
                middleRadius = (minRadius + maxRadius) / 2f;
                safetyNet = 0;

                numOfLoops++;
                if(numOfLoops >= maxNubOfLoops)
                {
                    Debug.Log("END GAME");
                }
            }

            currentAngle += goldenAngle;
            if (currentAngle > 2 * Mathf.PI)
            {
                currentAngle -= 2 * Mathf.PI;
            }

            //spawnPosition = new Vector2(middleRadius * Mathf.Cos(currentAngle), middleRadius * Mathf.Sin(currentAngle));
            spawnPosition = new Vector2(minRadius * Mathf.Cos(currentAngle), minRadius * Mathf.Sin(currentAngle));


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

    private List<GameObject> GetNearbyPonds()
    {
        string pondTag = GameManager.instance.pondTag;
        List<GameObject> ponds = new();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1000f);


        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.CompareTag(pondTag))
            {
                Debug.Log("found pond!");

                ponds.Add(collider.gameObject);
            }

        }

        return ponds;
    }
}
