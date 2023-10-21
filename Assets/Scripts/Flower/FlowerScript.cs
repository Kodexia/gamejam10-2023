using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;
    [SerializeField] GameObject flowerObject;

    public Flower flower;


    // Start is called before the first frame update
    void Awake()
    {
        flower = new Flower(gameObject, type, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
