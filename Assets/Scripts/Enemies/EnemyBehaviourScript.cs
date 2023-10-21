using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyStatsScript))]
public class EnemyBehaviourScript : MonoBehaviour
{
    EnemyStatsScript enemyStats;
    List<Flower> flowersInRange = new List<Flower>();
    Flower targetFlower;
    bool hasTarget { get { return (targetFlower != null); } }
    private void Start()
    {
        enemyStats = GetComponent<EnemyStatsScript>();
    }
    private void Update()
    {
        FindClosestFlowerWithPriority(3);
    }
    private Flower FindClosestFlowerWithPriority(int priority)
    {
        flowersInRange = GetNearbyFlowers();
        List<Flower> nearbyFlowers = flowersInRange.Where(flower => flower.Priority == priority).ToList();

        if (nearbyFlowers.Count == 0)
        {
            if (priority > 0)
                FindClosestFlowerWithPriority(priority-1);
        }

        nearbyFlowers = nearbyFlowers.OrderBy(flower => Vector3.Distance(transform.position, flower.flowerObject.transform.position)).ToList();

        return nearbyFlowers[0];
    }
    private List<Flower> GetNearbyFlowers()
    {
        List<Flower> nearbyFlowers = new();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyStats.attackRadius);
        foreach (Collider2D collider in colliders)
        {
            nearbyFlowers.Add(collider.gameObject.GetComponent<Flower>());
        }
        return nearbyFlowers;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, enemyStats.attackRadius);
    }
}