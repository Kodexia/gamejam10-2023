using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyStatsScript))]
public class EnemyBehaviourScript : MonoBehaviour
{
    [SerializeField] public Sprite[] directionalSprites; // Assuming you have 8 sprites in the order: N, NE, E, SE, S, SW, W, NW (9th is stationary)

    EnemyStatsScript enemyStats;
    List<FlowerScript> flowersInRange = new List<FlowerScript>();
    FlowerScript targetFlower = null;
    string flowerTag = "";
    MainFlowerScript mainFlower;
    Vector3 targetPos = Vector3.zero;
    bool hasTarget = false; //{ get { return (targetPos != null); } } NOT WORKING
    SpriteRenderer renderer;
    Animator animator;
    bool isAttacking = false;
    [SerializeField]
    AudioSource attackAudio;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStatsScript>();
        flowerTag = GameManager.instance.flowerTag;
        mainFlower = GameManager.instance.mainFlower;
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //if (!hasTarget)
            targetFlower = FindClosestFlowerWithPriority(3);
        //else
        //{
            UpdateSpriteDirection();

            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance <= 0.3f)
            {
                if (!isAttacking)
                {
                    hasTarget = false;
                    isAttacking = true;
                    StartCoroutine(Attack(targetPos - transform.position));
                    // implement the change of target on destroyed flower
                }
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemyStats.movementSpeed);
                isAttacking = false;
            }
        //}
    }
    private IEnumerator Attack(Vector3 direction)
    {
        while(isAttacking)
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
    private FlowerScript FindClosestFlowerWithPriority(int priority)
    {
        flowersInRange = GetNearbyFlowers();
        List<FlowerScript> nearbyFlowers = new();
        if (flowersInRange.Count > 0)
            nearbyFlowers = flowersInRange.Where(flower => flower.flower.Priority == priority).ToList();
        else
            if (priority > 0)
            FindClosestFlowerWithPriority(priority - 1);

        nearbyFlowers = nearbyFlowers.OrderBy(flower => Vector3.Distance(transform.position, flower.flower.flowerObject.transform.position)).ToList();
        if (nearbyFlowers.Count > 0)
        {
            SetTargetPosition(nearbyFlowers[0].flower.flowerObject.transform.position);
            return nearbyFlowers[0];
        }
        else if ((nearbyFlowers.Count <= 0) && GameObject.FindGameObjectsWithTag(GameManager.instance.flowerTag).Length > 0)
        {
            hasTarget = false;
            targetPos = mainFlower.transform.position;
            return null;
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
        //Gizmos.DrawSphere(transform.position, enemyStats.attackRadius);
    }
}