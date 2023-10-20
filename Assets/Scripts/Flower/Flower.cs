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
    int Health { get; set; }
    float Radius { get; set; }
    void TakeDamage(int amount);

}


public class Flower : IFlower
{
    public FlowerType Type { get; private set; }
    public int Health { get; set; }
    public float Radius { get; set; }


    public Flower(FlowerType type, float radius)
    {
        this.Type = type;
        this.Health = 100;
        this.Radius = radius;
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

    public FlowerMain(float radius) : base(FlowerType.Main, radius)
    {

    }

    public void ShootOutBud(string budType, Vector3 position)
    {

    }
}


