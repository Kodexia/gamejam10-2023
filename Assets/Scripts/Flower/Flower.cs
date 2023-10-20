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
    void TakeDamage(int amount, GameObject flower);

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

    public void TakeDamage(int amount, GameObject flower)
    {
        this.Health -= amount;

        if (this.Health <= 0)
        {
            DestroyFlower(flower);
        }
    }


    private void DestroyFlower(GameObject flower)
    {

    }
}

public class FlowerMain : Flower
{

    public FlowerMain(float radius) : base(FlowerType.Main, radius)
    {

    }

    public void ShootOutBud(string budType, Vector3 position, GameObject bud)
    {

    }
}


