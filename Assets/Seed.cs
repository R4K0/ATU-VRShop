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
    private GrowableSpot _touchedSpot;

    public void Plant(GrowableSpot spot)
    {
        spot.PlantDefinition = seed;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_touchedSpot != null)
            return;

        if (!collision.gameObject.TryGetComponent<ThirstySpot>(out var spot))
            return;

        _touchedSpot = spot;
        
        if (_touchedSpot.CanPlant(seed))
        {
            Plant(_touchedSpot);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (_touchedSpot == null)
            return;
        
        if (other.gameObject != _touchedSpot.gameObject)
            return;

        _touchedSpot = null;
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        
        Drop();

        if (_touchedSpot == null)
            return;

        if (_touchedSpot.CanPlant(seed))
        {
            Plant(_touchedSpot);
        }
    }
}
