using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
            foreach (GameObject nearestEnemy in enemiesInRadius)
            {
                GameObject newAlly = Instantiate(alliedPrefab, nearestEnemy.transform.position, Quaternion.identity);
                Destroy(nearestEnemy);
            }
        }
    }
}
