using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickableVegetable : Vegetable
{
    private bool _picked = false;
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (_picked)
            return;
        
        print("Picked up");

        if (TryGetComponent<HingeJoint>(out var joint))
        {
            Destroy(joint);
        }
        
        var vegetableTree = GetComponentInParent<VegetableTree>();
        if (!vegetableTree)
            return;
        
        vegetableTree.PickedVegetable(this);
        _picked = true;
    }
}