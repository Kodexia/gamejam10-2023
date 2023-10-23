using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeamlessBackground : MonoBehaviour
{
    [field: SerializeField] RawImage image;
    [field: SerializeField] float xAcceleration = -0.18f;
    [field: SerializeField] float yAcceleration = -0.17f;

    CharacterMovementScript _playerMovement;
    private void Start()
    {
        _playerMovement = GameManager.instance.playerBehaviour.GetComponent<CharacterMovementScript>();
    }
    void Update()
    {
        Vector2 acceleration = new Vector2(xAcceleration, yAcceleration);
        image.uvRect = new Rect(image.uvRect.position + (Vector2)Vector3.Normalize(_playerMovement.MoveDirection) * Time.deltaTime * acceleration, image.uvRect.size);
    }
}