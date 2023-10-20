using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;
    [SerializeField] GameObject flowerObject;
    [SerializeField] GameObject flowerBudPrefab;

    public FlowerMain flower;

    public MainFlowerScript() {
        flower = new FlowerMain(flowerObject, radius);
    }


    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the flower!");
        }
    }

    //on water fill

    void NewBud()
    {
        flower.ShootOutBud(FlowerType.Economic, new Vector2(1, 1), flowerBudPrefab);
    }
}
