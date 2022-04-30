using System;
using UnityEngine;

public class ThirstySpot : GrowableSpot, IThirstySpot
{
    [Header("Spot Thirst")]
    [SerializeField] private float water = 100f;
    [SerializeField] private float thirstRate = 1f;
    public override bool CanGrow()
    {
        if (water <= 0f)
            return false;
        
        return base.CanGrow();
    }

    public void AddWater(float waterToAdd)
    {
        water = Math.Min(water + waterToAdd, 100);
    }

    public override void DoGrow()
    {
        base.DoGrow();

        water -= Time.deltaTime * thirstRate;
    }
}