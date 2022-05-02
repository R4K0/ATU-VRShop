using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickableVegetable : Vegetable
{
    private bool _picked = false;
    [SerializeField] private VegetableTree tree;
    
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
        
        if (tree == null)
            return;
        
        tree.PickedVegetable(this);
        _picked = true;
    }
}