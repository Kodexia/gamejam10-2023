using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;
    [SerializeField] GameObject flowerObject;

    [SerializeField] GameObject flowerBudPrefabOffensive;
    [SerializeField] GameObject flowerBudPrefabDefensive;
    [SerializeField] GameObject flowerBudPrefabEconomic;
    GrassGrowth grassGrowth;

    public FlowerMain flower;

    public MainFlowerScript() {
        flower = new FlowerMain(flowerObject, radius);
    }


    void Start()
    {
        grassGrowth = GetComponentInChildren<GrassGrowth>();
        Debug.Log(flower.Health);
    }

    void Update()
    {
        testDmg();
    }

    void testDmg()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flower.TakeDamage(10);
            grassGrowth.ChangeScale();
            Debug.Log(flower.Health);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            NewBud(flowerBudPrefabEconomic); 

            Debug.Log("Player has collided with the flower!");
        }
    }

    //on water fill

    void NewBud(GameObject flowerBudPrefab)
    {
        flower.ShootOutBud(new Vector2(1, 1), flowerBudPrefab);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
