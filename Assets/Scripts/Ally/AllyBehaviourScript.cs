using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;

[RequireComponent(typeof(EnemyStatsScript))]
public class AllyBehaviourScript : MonoBehaviour
{
    EnemyStatsScript enemyStats;
    List<GameObject> enemiesInRange = new List<GameObject>();
    GameObject targetEnemy;

    MainFlowerScript mainFlower;
    Vector3 targetPos = Vector3.zero;

    bool hasTarget = false; //{ get { return (targetPos != null); } } NOT WORKING

    string enemyTag;
    private void Start()
    {
        enemyStats = GetComponent<EnemyStatsScript>();

        mainFlower = GameManager.instance.mainFlower;
        enemyTag = GameManager.instance.enemyTag;
    }
    private void Update()
    {
        if (!hasTarget)
            targetEnemy = FindClosestEnemy();
        else
        {

            Vector2 newPos = targetEnemy.transform.position;

            transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * enemyStats.movementSpeed);

            float distance = Vector3.Distance(transform.position, targetPos);

            if (distance <= 0.3f)
            {
                hasTarget = false;
                Debug.Log("In range!");
                // Add Attack
            }
        }
    }

    private void SetTargetPosition(Vector3 pos)
    {
        targetPos = pos;
        hasTarget = true;
    }

    private GameObject FindClosestEnemy()
    {
        enemiesInRange = GetNearbyEnemies();

        List<GameObject> nearbyEnemies = new();


        if (enemiesInRange.Count > 0)
        {
            nearbyEnemies = enemiesInRange;
        }

        nearbyEnemies = nearbyEnemies.OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).ToList();

        if (nearbyEnemies.Count > 0)
        {
            SetTargetPosition(nearbyEnemies[0].transform.position);

            return nearbyEnemies[0];
        }
        else
        {
            return null;
        }
    }
    private List<GameObject> GetNearbyEnemies()
    {
        List<GameObject> nearbyEnemies = new();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyStats.attackRadius);


        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                Debug.Log("found enemy!");

                nearbyEnemies.Add(collider.gameObject);
            }

        }

        return nearbyEnemies;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, enemyStats.attackRadius);
    }
}