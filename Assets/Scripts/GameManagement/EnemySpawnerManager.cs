using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public static EnemySpawnerManager instance;

    public GameObject enemyPrefab;
    public float initialDelay = 2.0f;
    public float spawnRate = 1.0f;
    public Vector2 mapSize = new Vector2(10f, 5f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", initialDelay, spawnRate);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomBorderPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    Vector2 GetRandomBorderPosition()
    {
        int side = Random.Range(0, 4);

        float xPosition = 0f;
        float yPosition = 0f;

        switch (side)
        {
            case 0: // top
                xPosition = Random.Range(-mapSize.x / 2, mapSize.x / 2);
                yPosition = mapSize.y / 2;
                break;
            case 1: // bottom
                xPosition = Random.Range(-mapSize.x / 2, mapSize.x / 2);
                yPosition = -mapSize.y / 2;
                break;
            case 2: // left
                xPosition = -mapSize.x / 2;
                yPosition = Random.Range(-mapSize.y / 2, mapSize.y / 2);
                break;
            case 3: // right
                xPosition = mapSize.x / 2;
                yPosition = Random.Range(-mapSize.y / 2, mapSize.y / 2);
                break;
        }

        return new Vector2(xPosition, yPosition);
    }

    public void ChangeSpawnRate(float newRate)
    {
        CancelInvoke("SpawnEnemy");
        spawnRate = newRate;
        InvokeRepeating("SpawnEnemy", 0, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnEnemy");
    }
}

