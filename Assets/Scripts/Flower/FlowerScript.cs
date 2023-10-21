using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBarScript))]

public abstract class FlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;
    [SerializeField] GameObject flowerObject;
    [SerializeField] float maxHealth = 100f;
    GrassGrowth grassGrowth;

    public Flower flower;
    private HealthBarScript barScript;


    // Start is called before the first frame update
    void Awake()
    {
        flower = new Flower(gameObject, type, radius, maxHealth);
        barScript = GetComponent<HealthBarScript>();
        grassGrowth = GetComponentInChildren<GrassGrowth>();
        FlowerStart();
    }

    // Update is called once per frame
    void Update()
    {
        FlowerUpdate();
        barScript.UpdateHealthbar(flower.Health, maxHealth);
    }

    protected abstract void FlowerStart();
    protected abstract void FlowerUpdate();
}
