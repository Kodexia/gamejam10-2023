using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    EnemyStatsScript stats;
    private void Start()
    {
        stats = transform.parent.GetComponent<EnemyStatsScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.collider.CompareTag(GameManager.instance.flowerTag))
            collision.collider.GetComponent<FlowerScript>().TakeDamage(stats.attackDamage);
        else if (collision.collider.CompareTag(GameManager.instance.mainFlowerTag))
            collision.collider.GetComponent<MainFlowerScript>().TakeDamage(stats.attackDamage);
    }
}