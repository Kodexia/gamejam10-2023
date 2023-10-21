
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudMoveInArc : MonoBehaviour
{
    public float duration = 1.0f; // Duration of the movement
    public float heightOffset = 5.0f; // How high the arc should be
    private GameObject _bud;
    private GameObject _flower;

    public void ShootOutBud(Vector2 startingPosition, Vector2 endPosition, GameObject bud, GameObject flower)
    {
        Debug.Log("Shoot out bud v2 - MoveInArc");
        StartCoroutine(MoveObjectInArc(startingPosition, endPosition));
        _bud = bud;
        _flower = flower;
    }

    private IEnumerator MoveObjectInArc(Vector2 startingPosition, Vector2 endPosition)
    {
        float elapsedTime = 0;
        Vector2 vertex = (startingPosition + endPosition) / 2;
        vertex.y += heightOffset;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Lerp between start and vertex, and vertex and end based on t
            Vector2 firstLerp = Vector2.Lerp(startingPosition, vertex, t);
            Vector2 secondLerp = Vector2.Lerp(vertex, endPosition, t);
            
            // Lerp between the above two Lerp results to get the position on the arc
            _bud.transform.position = Vector2.Lerp(firstLerp, secondLerp, t);

            yield return null;
        }

        GameObject newBudObject = Instantiate(_flower, endPosition, Quaternion.identity);
    }
}
