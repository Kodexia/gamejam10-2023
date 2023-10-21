using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OffensiveFlower : FlowerScript
{
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
            
            Collider[] hitColliders = new Collider[50]; // Random jsem dal velikost, pokud by nestačilo stačí to zvětšit

            Vector3 flowerPosition = flower.flowerObject.transform.position;
            float radius = flower.Radius;

            int numColliders = Physics.OverlapSphereNonAlloc(flowerPosition, radius, hitColliders);

            List<GameObject> enemiesInRadius = new List<GameObject>();

            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].CompareTag("Enemy"))
                {
                    enemiesInRadius.Add(hitColliders[i].gameObject);
                }
            }
        
            List<GameObject> nearestEnemies = enemiesInRadius
                .OrderBy(enemy => Vector3.Distance(enemy.transform.position, flowerPosition))
                .Take(numberOfEnemies)
                .ToList();
        
            // ToDo: Convert first numberOfEnemies enemies to friendly
        }
    }
}
