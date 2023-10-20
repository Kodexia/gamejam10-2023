using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainFlowerScript : MonoBehaviour
{
    [SerializeField] float radius = 20f;
    [SerializeField] FlowerType type;

    public Flower flower;


    // Start is called before the first frame update
    void Start()
    {
        flower = new Flower(type, radius);
    }

    // Update is called once per frame
    void Update()
    {
        // check for collision
        //  player - water
        //  
        

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
