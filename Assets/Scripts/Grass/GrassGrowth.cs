using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrassGrowth : MonoBehaviour
{
    [SerializeField]
    Transform grassScale;

    Transform originGrassScale;
    [SerializeField]
    MainFlowerScript flowerScript;
    Flower flower;
    
    
    // Start is called before the first frame update
    void Start()
    {
        originGrassScale = grassScale;
        Debug.Log(flowerScript.flower.Health);
        flower = flowerScript.flower;
    }



    // Changes scaling of object based on left hp of parent object in %
    public void ChangeScale()
    {
        float procent = flower.Health / flower.MaxHealth;
        grassScale.localScale = new Vector3(procent, procent, procent);
        Debug.Log(grassScale.localScale.x);

    }
}
