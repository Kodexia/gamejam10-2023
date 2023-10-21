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
    GameObject targetFlower = null;

    MainFlowerScript mainFlower;
    Vector3 targetPos = Vector3.zero;

    bool hasTarget = false; //{ get { return (targetPos != null); } } NOT WORKING

    string flowerTag;
    string allyTag;
    string enemyTag;
    private void Start()
    {
        enemyStats = GetComponent<EnemyStatsScript>();

        flowerTag = GameManager.instance.flowerTag;
        mainFlower = GameManager.instance.mainFlower;

        allyTag = GameManager.instance.allyTag;
        enemyTag = GameManager.instance.enemyTag;
    }
    private void Update()
    {
        Debug.Log(hasTarget);
        if (!hasTarget)
            targetFlower = FindClosestEnemy(); //change
        else
        {
            Debug.Log("moving-------------------------------------------------------------");
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
        Debug.Log("Called!!!!!!!!!!!");
        targetPos = pos;
        hasTarget = true;
    }

    private GameObject FindClosestEnemy()
    {
        enemiesInRange = GetNearbyEnemies();

        List<GameObject> nearbyEnemies = new();

        //Debug.Log($"Count: {enemiesInRange.Count}");


        if (enemiesInRange.Count > 0)
        {
            nearbyEnemies = enemiesInRange;
        }

        nearbyEnemies = nearbyEnemies.OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)).ToList();

        if (nearbyEnemies.Count > 0)
        {
            SetTargetPosition(nearbyEnemies[0].transform.position); //chage if doesnt work

            return nearbyEnemies[0];
        }
        else
        {
            SetTargetPosition(mainFlower.transform.position);
            return null;
        }
    }
    private List<GameObject> GetNearbyEnemies()
    {
        Debug.Log("_____________________________________________________________");
        List<GameObject> nearbyEnemies = new();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyStats.attackRadius);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider.tag + " " + collider.transform.position);
            if (collider.CompareTag(enemyTag))
            {
                Debug.Log("found enemy!");

                nearbyEnemies.Add(collider.gameObject);
            }
                
        }
        Debug.Log(nearbyEnemies[0].transform.position);

        return nearbyEnemies;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, enemyStats.attackRadius);
    }
}
