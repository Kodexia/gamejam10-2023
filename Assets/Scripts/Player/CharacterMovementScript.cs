using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    CharacterStatsScript stats;
    Vector3 movePosition;
    public Action<string> onEnemyTarget;
    void Start()
    {
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

            // Checker, if you hit an enemy
            RaycastHit2D clickRaycast = Physics2D.Raycast(mousePositionInWorld, Vector3.forward * 50, 50);
            if (clickRaycast.collider != null)
                if (clickRaycast.collider.CompareTag(stats.enemyTag))
                    onEnemyTarget?.Invoke(clickRaycast.collider.name);
        }

        // Move, if you have a point to go
        if (ShouldMove())
            transform.position = Vector3.MoveTowards(transform.position, movePosition, Time.deltaTime * stats.movementSpeed);
    }
    // This method returns bool depending on if the player should be moving somewhere or not
    private bool ShouldMove()
    {
        bool isOnPosition = (transform.position == movePosition);
        return !isOnPosition;
    }
}