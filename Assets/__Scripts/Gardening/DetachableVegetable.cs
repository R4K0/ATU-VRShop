using System;
using UnityEngine.XR.Interaction.Toolkit;

public class DetachableVegetable : Vegetable
{
    [NonSerialized]
    public GrowableSpot spot;
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (spot != null)
        {
            spot.DetachPlant();
        }
    }
}