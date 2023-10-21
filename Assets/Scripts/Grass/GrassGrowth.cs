using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrassGrowth : MonoBehaviour
{
    [SerializeField]
    Transform grassScale;
    [SerializeField]
    FlowerScript flowerScript;
    Flower flower;

    [SerializeField]
    float initialBonusScaling = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(initialBonusScaling, initialBonusScaling,initialBonusScaling);
        Debug.Log(flowerScript.flower.Health);
        flower = flowerScript.flower;
    }



    // Changes scaling of object based on left hp of parent object in %
    public void ChangeScale()
    {
        if(flower.Health >= 0)
        {
            float procent = flower.Health / flower.MaxHealth;
            grassScale.localScale = new Vector3(procent, procent, procent);
        }
        Debug.Log("scaling of alive grass: " + grassScale.localScale.x);

    }
}
