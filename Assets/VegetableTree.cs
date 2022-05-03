using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableTree : MonoBehaviour
{
    public List<Vegetable> Vegetables;
    
    public void PickedVegetable(PickableVegetable vegetable)
    {
        if (!Vegetables.Remove(vegetable)) return;

        vegetable.gameObject.transform.SetParent(null);
        
        if (Vegetables.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
