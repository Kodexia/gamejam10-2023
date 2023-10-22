using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveFlower : FlowerScript
{
    [SerializeField]
    private GameObject alliedPrefab;
    
    protected override void FlowerStart()
    {
        StartCoroutine(ConvertEnemiesInRadius(5));
    }

    protected override void FlowerUpdate()
    {
        // Isn't needed right now
    }

    IEnumerator ConvertEnemiesInRadius(int numberOfEnemies, int delay = 30)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            List<GameObject> enemiesInRadius = new List<GameObject>();
            foreach (GameObject enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, flower.flowerObject.transform.position) <= flower.Radius)
                {
                    enemiesInRadius.Add(enemy);
                }
            }
        
            ShuffleList(enemiesInRadius);

            int enemiesToConvert = Mathf.Min(numberOfEnemies, enemiesInRadius.Count);
            for (int i = 0; i < enemiesToConvert; i++)
            {
                GameObject enemyToConvert = enemiesInRadius[i];
                GameObject newAlly = Instantiate(alliedPrefab, enemyToConvert.transform.position, Quaternion.identity);
                Destroy(enemyToConvert);
            }
        }
    }
    
    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
