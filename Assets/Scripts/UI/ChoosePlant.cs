using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlant : MonoBehaviour
{
    private MainFlowerScript _script;
    
    void Start()
    {
        _script = GameManager.instance.mainFlower;
    }

    public void SpawnAttackFlower()
    {
        Debug.Log("Spawned attack");
        _script.NewBud(GameManager.instance.flowerBudPrefabOffensive);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    public void SpawnDefensiveFlower()
    {
        Debug.Log("Spawned defensive");
        _script.NewBud(GameManager.instance.flowerBudPrefabDefensive);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    public void SpawnEconomicFlower()
    {
        Debug.Log("Spawned economic");
        _script.NewBud(GameManager.instance.flowerBudPrefabEconomic);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

}
