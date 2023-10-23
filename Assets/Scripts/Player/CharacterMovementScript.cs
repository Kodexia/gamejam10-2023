using System;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    CharacterStatsScript stats;
    Vector3 movePosition;
    public Action<EnemyBehaviourScript> onEnemyTarget;

    [SerializeField] public Sprite[] directionalSprites; // Assuming you have 8 sprites in the order: N, NE, E, SE, S, SW, W, NW (9th is stationary)
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    AudioSource moveAudio;

  void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStatsScript>();
        movePosition = transform.position;
    }

    void Update()
    {
        // Checks if you pressed the right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Getting the mouse position and setting it as a position the player wants to go to
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movePosition = mousePositionInWorld;
            movePosition.z = 0;
            if (moveAudio != null && moveAudio.isPlaying == false) moveAudio.Play();

            // Checker, if you hit an enemy
            RaycastHit2D clickRaycast = Physics2D.Raycast(mousePositionInWorld, Vector3.forward * 50, 50);
            if (clickRaycast.collider != null)
                if (clickRaycast.collider.CompareTag(stats.enemyTag))
                    onEnemyTarget?.Invoke(clickRaycast.collider.GetComponent<EnemyBehaviourScript>());
        }

        // Move, if you have a point to go
        if (ShouldMove())
        {
            transform.position = Vector3.MoveTowards(transform.position, movePosition, Time.deltaTime * stats.movementSpeed);
            UpdateSpriteDirection();
        }
        //else if(!ShouldMove())
        //{ 
        //    spriteRenderer.sprite = directionalSprites[8];
        //}
        //else
        //spriteRenderer.sprite = directionalSprites[8];
    }
    // This method returns bool depending on if the player should be moving somewhere or not
    private bool ShouldMove()
    {
        bool isOnPosition = (transform.position == movePosition);
        if(isOnPosition) moveAudio.Stop(); 
        return !isOnPosition;
    }
    private void UpdateSpriteDirection()
    {
        Vector3 direction = movePosition - transform.position;
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

        spriteRenderer.sprite = directionalSprites[spriteIndex];
    }
    public void StopMovement()
    {
        movePosition = transform.position;
    }
}