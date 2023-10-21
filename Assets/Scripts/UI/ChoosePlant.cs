using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlant : MonoBehaviour
{
    [SerializeField]
    MainFlowerScript script;
    public void SpawnAttackFlower()
    {
        Debug.Log("Spawned attack");
        script.NewBud(script.flowerBudPrefabOffensive);
    }
    public void SpawnDefensiveFlower()
    {
        Debug.Log("Spawned defensive");
        script.NewBud(script.flowerBudPrefabDefensive);
    }
    public void SpawnEconomicFlower()
    {
        Debug.Log("Spawned economic");
        script.NewBud(script.flowerBudPrefabEconomic);
    }

}
