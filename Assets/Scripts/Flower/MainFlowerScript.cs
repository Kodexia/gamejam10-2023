using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;

    public Flower flower;


    void Start()
    {
        flower = new Flower(type, radius);
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the flower!");
        }

        else if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("Player has collected water!");
        }
    }
}
