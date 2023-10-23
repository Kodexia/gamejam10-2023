using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeamlessBackground : MonoBehaviour
{
    [field: SerializeField] RawImage image;
    [field: SerializeField] float acceleration = 1f;

    CharacterMovementScript _playerMovement;
    private void Start()
    {
        _playerMovement = GameManager.instance.playerBehaviour.GetComponent<CharacterMovementScript>();
    }
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + (Vector2)Vector3.Normalize(_playerMovement.MoveDirection) * Time.deltaTime * acceleration, image.uvRect.size);
    }
}
