using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerType
{
    Main,
    Attack,
    Defense,
    Economic
}

public interface IFlower
{
    FlowerType Type { get; }
    float Health { get; set; }
    float MaxHealth { get; set; }
    float Radius { get; set; }
    void TakeDamage(int amount);
    GameObject flowerObject {  get; }

}


public class Flower : IFlower
{
    public FlowerType Type { get; private set; }

    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Radius { get; set; }

    public GameObject flowerObject { get; private set; }


    public Flower(GameObject flower, FlowerType type, float radius, int maxHealth = 100)
    {
        this.Type = type;
        this.Health = 100f;
        this.Radius = radius;
        this.flowerObject = flower;
        MaxHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        this.Health -= amount;

        if (this.Health <= 0)
        {
            DestroyFlower();
        }
        
    }


    private void DestroyFlower()
    {
        
    }
}

public class FlowerMain : Flower
{

    public FlowerMain(GameObject flower, float radius) : base(flower, FlowerType.Main, radius)
    {

    }

    public void ShootOutBud(Vector3 position, GameObject budPrefab)
    {
        GameObject newBud = GameObject.Instantiate(budPrefab, position, Quaternion.identity);

    }
}


