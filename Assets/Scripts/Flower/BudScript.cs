using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BudScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;

    public Flower flower;


    // Start is called before the first frame update
    void Start()
    {
        flower = new Flower(type, radius);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
