using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveFlower : FlowerScript
{
    // Start is called before the first frame update
    protected override void FlowerStart()
    {
        flower.Priority = 3;
    }

    protected override void FlowerUpdate()
    {
        // Isn't needed right now
    }
}
