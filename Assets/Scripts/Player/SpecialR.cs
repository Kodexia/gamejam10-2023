using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialR : MonoBehaviour
{
    [field: SerializeField] private float radius = 1f;
    [field: SerializeField] private float delay = 0.1f;
    [field: SerializeField] private CharacterStatsScript playerStats;
    [field: SerializeField] private ParticleSystem particles;
    private void OnEnable() //Attack
    {
        particles.Stop();
        Invoke("Attack", delay);
    }
    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(GameManager.instance.enemyTag))
                hit.GetComponent<EnemyStatsScript>().GetHit(playerStats.specialRDamage);
        }
        particles.Play();
        while (particles.isPlaying){ }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}