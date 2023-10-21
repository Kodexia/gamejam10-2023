using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlant : MonoBehaviour
{
    [SerializeField]
    MainFlowerScript script;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void SpawnAttackFlower()
    {
        Debug.Log("Spawned attack");
        script.NewBud(script.flowerBudPrefabOffensive);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    public void SpawnDefensiveFlower()
    {
        Debug.Log("Spawned defensive");
        script.NewBud(script.flowerBudPrefabDefensive);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);

    }
    public void SpawnEconomicFlower()
    {
        Debug.Log("Spawned economic");
        script.NewBud(script.flowerBudPrefabEconomic);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

}
