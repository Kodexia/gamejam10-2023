using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyStatsScript))]
public class EnemyBehaviourScript : MonoBehaviour
{
    EnemyStatsScript enemyStats;
    List<FlowerScript> flowersInRange = new List<FlowerScript>();
    FlowerScript targetFlower = null;
    string flowerTag = "";
    MainFlowerScript mainFlower;
    Vector3 targetPos = Vector3.zero;
    bool hasTarget = false; //{ get { return (targetPos != null); } } NOT WORKING
    private void Start()
    {
        enemyStats = GetComponent<EnemyStatsScript>();
        flowerTag = GameManager.instance.flowerTag;
        mainFlower = GameManager.instance.mainFlower;
    }
    private void Update()
    {
        if (!hasTarget)
            targetFlower = FindClosestFlowerWithPriority(3);
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemyStats.movementSpeed);

            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance <= 0.3f)
            {
                hasTarget = false;
                Debug.Log("In range!");
                // implement the change of target on destroyed flower
            }
        }
    }
    private void SetTargetPosition(Vector3 pos)
    {
        targetPos = pos;
        hasTarget = true;
    }
    private FlowerScript FindClosestFlowerWithPriority(int priority)
    {
        flowersInRange = GetNearbyFlowers();
        List<FlowerScript> nearbyFlowers = new();
        Debug.Log($"Count: {flowersInRange.Count}");
        if (flowersInRange.Count > 0)
            nearbyFlowers = flowersInRange.Where(flower => flower.flower.Priority == priority).ToList();
        else
            if (priority > 0)
                FindClosestFlowerWithPriority(priority-1);

        nearbyFlowers = nearbyFlowers.OrderBy(flower => Vector3.Distance(transform.position, flower.flower.flowerObject.transform.position)).ToList();
        if (nearbyFlowers.Count > 0)
        {
            SetTargetPosition(nearbyFlowers[0].flower.flowerObject.transform.position);
            return nearbyFlowers[0];
        }
        else
        {
            SetTargetPosition(mainFlower.transform.position);
            return null;
        }
    }
    private List<FlowerScript> GetNearbyFlowers()
    {
        List<FlowerScript> nearbyFlowers = new();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyStats.attackRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(flowerTag))
                nearbyFlowers.Add(collider.gameObject.GetComponent<FlowerScript>());
        }
        return nearbyFlowers;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, enemyStats.attackRadius);
    }
}