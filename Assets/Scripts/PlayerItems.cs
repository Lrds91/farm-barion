using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amounts")]
    public int totalWood;
    public int carrots;
    public int currentWater;
    public int fishes;

    [Header("Limits")]
    public float waterLimit = 20;
    public float woodLimit = 100;
    public float carrotLimit = 100;
    public float fishesLimit = 20;

    public void WaterLimit(int water)
    {
        if(currentWater < waterLimit)
        {
            currentWater += water;
        }
        
    }

}
