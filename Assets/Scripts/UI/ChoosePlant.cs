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
        script.NewBud(GameManager.instance.flowerBudPrefabOffensive);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    public void SpawnDefensiveFlower()
    {
        Debug.Log("Spawned defensive");
        script.NewBud(GameManager.instance.flowerBudPrefabDefensive);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    public void SpawnEconomicFlower()
    {
        Debug.Log("Spawned economic");
        script.NewBud(GameManager.instance.flowerBudPrefabEconomic);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

}
