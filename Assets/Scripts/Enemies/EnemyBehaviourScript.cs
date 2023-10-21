using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStatsScript))]
public class EnemyBehaviourScript : MonoBehaviour
{
    EnemyStatsScript enemyStats;
    List<Flower> flowersInRange = new List<Flower>();
    private void Start()
    {
        enemyStats = GetComponent<EnemyStatsScript>();
    }
    private void Update()
    {
        UpdateList();
    }
    private void UpdateList()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, enemyStats.attackRadius);
        SortTargetArray(ref cols);
    }
    private void SortTargetArray(ref Collider2D[] cols)
    {
        for (int i = 0; i < cols.Length - 1; i++)
        {
            for (int j = 0; j < cols.Length - i - 1; i++)
            {
                if (GetDistance(cols[j].transform.position) > GetDistance(cols[j + 1].transform.position))
                {
                    Collider2D temp = cols[j];
                    cols[j] = cols[j + 1];
                    cols[j + 1] = temp;
                }
            }
        }
    }
    private float GetDistance(Vector3 obj)
    {
        return (Vector3.Distance(transform.position, obj));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, enemyStats.attackRadius);
    }
}