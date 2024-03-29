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
    float WaterLevel { get; set; }
    float MaxWaterLevel { get; set; }
    float Radius { get; set; }
    public float Priority { get; set; }

    bool TakeDamage(float amount);
    void increaseWaterLevel(float amount);
    void decreaseWaterLevel(float amount);
    GameObject flowerObject { get; }

}


public class Flower : IFlower
{
    public FlowerType Type { get; private set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float WaterLevel { get; set; }
    public float MaxWaterLevel { get; set; }
    public float Radius { get; set; }
    public float Priority { get; set; }
    public GameObject flowerObject { get; private set; }

    public GrassGrowth grassScript;



    public Flower(GameObject flower, FlowerType type, float radius, float maxHealth = 100, float priority = 3)
    {
        this.Type = type;
        this.Health = maxHealth;
        this.Radius = radius;
        this.flowerObject = flower;
        this.Priority = priority;
        MaxHealth = maxHealth;
    }

    public bool TakeDamage(float amount)
    {
        this.Health -= amount;

        if (this.Health <= 0)
        {
            DestroyFlower();
            return true;
        }
        else
            return false;
        
    }

    public void increaseWaterLevel(float amount)
    {
        if (this.WaterLevel + amount > MaxWaterLevel)
        {
            this.WaterLevel = MaxWaterLevel;
        }
        else
        {
            this.WaterLevel += amount;
        }
    }

    public void decreaseWaterLevel(float amount)
    {
        if (this.WaterLevel - amount < 0)
        {
            this.WaterLevel = 0;
        }
        else
        {
            this.WaterLevel -= amount;
        }
    }


    public void DestroyFlower()
    {
       
    }
}

public class FlowerMain : Flower
{

    public FlowerMain(GameObject flower, float radius, int priority = 3, float hp = 100) : base(flower, FlowerType.Main, radius, hp, priority)
    {

    }
}


