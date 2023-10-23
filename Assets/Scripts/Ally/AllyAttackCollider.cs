using UnityEngine;

public class AllyAttackCollider : MonoBehaviour
{
    CharacterStatsScript stats;
    AllyBehaviourScript myStats;

    private void Start()
    {
        stats = GameManager.instance.playerBehaviour.GetComponent<CharacterStatsScript>();
        myStats = transform.parent.GetComponent<AllyBehaviourScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(GameManager.instance.enemyTag))
            if (collision.collider.GetComponent<EnemyStatsScript>().GetHit(stats.attackDamage / 2))
                myStats.Killed();

    }
}