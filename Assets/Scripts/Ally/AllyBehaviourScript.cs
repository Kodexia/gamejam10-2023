using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyStatsScript))]
public class AllyBehaviourScript : MonoBehaviour
{
    [SerializeField] public Sprite[] directionalSprites; // Assuming you have 8 sprites in the order: N, NE, E, SE, S, SW, W, NW (9th is stationary)
    [field: SerializeField] private int killsUntilDeath { get; set; } = 2;
    EnemyStatsScript enemyStats;
    List<GameObject> enemiesInRange = new List<GameObject>();
    GameObject targetEnemy;

    MainFlowerScript mainFlower;
    Vector3 targetPos = Vector3.zero;

    bool hasTarget = false; //{ get { return (targetPos != null); } } NOT WORKING
    bool isAttacking = false;
    Animator animator;
    SpriteRenderer renderer;
    [SerializeField]
    AudioSource attackAudio;

    string enemyTag;
    private void Start()
    {
        enemyStats = GetComponent<EnemyStatsScript>();

        mainFlower = GameManager.instance.mainFlower;
        enemyTag = GameManager.instance.enemyTag;
        animator = gameObject.GetComponent<Animator>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!hasTarget)
            targetEnemy = FindClosestEnemy();
        else
        {
            Vector3 targetPos = targetEnemy.transform.position;
            UpdateSpriteDirection();

            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance <= 0.3f)
            {
                if (!isAttacking)
                {
                    hasTarget = false;
                    isAttacking = true;
                    StartCoroutine(Attack(targetPos - transform.position));
                    Debug.Log("In range!");
                    // implement the change of target on destroyed flower
                }
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemyStats.movementSpeed);
                isAttacking = false;
            }
        }
    }
    private IEnumerator Attack(Vector3 direction)
    {
        while (isAttacking)
        {
            Vector3 dir = Vector3.Normalize(direction);

            animator.SetFloat("Horizontal", dir.x);
            animator.SetFloat("Vertical", dir.y);
            attackAudio.Play();
            animator.SetTrigger("Attack");

            yield return new WaitForSeconds(enemyStats.attackDelay);
        }

        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
    }
    public void Killed()
    {
        isAttacking = false;
        hasTarget = false;
        killsUntilDeath--;
        if (killsUntilDeath <= 0)
            enemyStats.GetHit(enemyStats.maxHp);
    }
    private void UpdateSpriteDirection()
    {
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        int spriteIndex = 0;
        if (angle < -157.5f && angle >= 157.5f)
            spriteIndex = 0; // LEFT
        else if (angle < 157.5f && angle >= 112.5f)
            spriteIndex = 1; // TOP-LEFT
        else if (angle < 112.5f && angle >= 67.5f)
            spriteIndex = 2; // TOP
        else if (angle < 67.5f && angle >= 22.5f)
            spriteIndex = 3; // TOP-RIGHT
        else if (angle < 22.5f && angle >= -22.5)
            spriteIndex = 4; // RIGHT
        else if (angle < -22.5f && angle >= -67.5f)
            spriteIndex = 5; // BOTTOM-RIGHT
        else if (angle < -67.5f && angle >= -112.5f)
            spriteIndex = 6; // bottom
        else if (angle < -112.5f && angle >= -157.5f)
            spriteIndex = 7; // bottom-left

        renderer.sprite = directionalSprites[spriteIndex];
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