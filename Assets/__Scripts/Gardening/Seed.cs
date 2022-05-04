using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IPlantable
{
    void Plant(GrowableSpot spot);
}

public class Seed : XRGrabInteractable, IPlantable
{
    [Header("Seed Setup")]
    public PlantDefinition seed;

    public void Plant(GrowableSpot spot)
    {
        spot.PlantDefinition = seed;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spot = collision.gameObject.GetComponentInParent<ThirstySpot>();
        if (!spot)
            return;
        
        if (spot.CanPlant(seed))
        {
            Plant(spot);
        }
    }
}
