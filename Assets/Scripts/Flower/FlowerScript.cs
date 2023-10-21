using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class FlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;
    [SerializeField] GameObject flowerObject;
    [SerializeField] private float maxHealth = 100f;
    GrassGrowth grassGrowth;

    public Flower flower;


    // Start is called before the first frame update
    void Awake()
    {
        flower = new Flower(gameObject, type, radius, maxHealth);
        grassGrowth = GetComponentInChildren<GrassGrowth>();
        FlowerStart();
    }

    // Update is called once per frame
    void Update()
    {
        FlowerUpdate();
    }

    protected abstract void FlowerStart();
    protected abstract void FlowerUpdate();
}
