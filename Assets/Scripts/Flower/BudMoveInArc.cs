using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudMoveInArc : MonoBehaviour
{
    public float duration = 1.0f; // Duration of the movement
    public float heightOffset = 5.0f; // How high the arc should be

    public void ShootOutBud(Vector2 startingPosition, Vector2 endPosition, GameObject seed, GameObject flower)
    {
        GameObject budInstance = Instantiate(seed, startingPosition, Quaternion.identity);
        StartCoroutine(MoveObjectInArc(budInstance, flower, startingPosition, endPosition));
    }

    private IEnumerator MoveObjectInArc(GameObject budInstance, GameObject flowerInstance, Vector2 startingPosition, Vector2 endPosition)
    {
        float elapsedTime = 0;
        Vector2 vertex = (startingPosition + endPosition) / 2;
        vertex.y += heightOffset;
        
        Vector2 lastPosition = startingPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            
            Vector2 firstLerp = Vector2.Lerp(startingPosition, vertex, t);
            Vector2 secondLerp = Vector2.Lerp(vertex, endPosition, t);

            Vector2 currentPosition = Vector2.Lerp(firstLerp, secondLerp, t);
            budInstance.transform.position = currentPosition;

            Vector2 deltaPosition = currentPosition - lastPosition;
            float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;
            budInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
            
            lastPosition = currentPosition;

            yield return null;
        }

        GameObject newBudObject = Instantiate(flowerInstance, endPosition, Quaternion.identity);
        Destroy(budInstance);
    }
}