using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    CharacterStatsScript stats;
    private void Start()
    {
        stats = GameManager.instance.playerBehaviour.GetComponent<CharacterStatsScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.collider.CompareTag(GameManager.instance.enemyTag))
            collision.collider.GetComponent<EnemyStatsScript>().GetHit(stats.attackDamage/2);
    }
}