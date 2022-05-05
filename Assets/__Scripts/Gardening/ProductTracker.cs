using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class ProductTracker : MonoBehaviour
{
    public List<Vegetable> trackedProducts { private set; get; } = new List<Vegetable>();

    public UnityEvent productsUpdated;
    public UnityEvent moneyUpdated;

    public int moneyEarned { private set; get; } = 0;
    public bool ProcessNpcPurchase()
    {
        if (trackedProducts.Count(vegetable => vegetable != null) == 0)
            return false;

        var soldAny = false;
        foreach (var vegetable in trackedProducts.Where(vegetable => vegetable != null)
                     .OrderBy(vegetable => Guid.NewGuid())
                     .Take(UnityEngine.Random.Range(1, 3)))
        {
            moneyEarned += vegetable.vegetable.sellPrice;
            trackedProducts.Remove(vegetable);
            Destroy(vegetable.gameObject);
            
            soldAny = true;
        }

        if (soldAny)
        {
            productsUpdated.Invoke();
            moneyUpdated.Invoke();
        }
        
        return soldAny;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Vegetable>(out var vegetable) && !trackedProducts.Contains(vegetable))
        {
            trackedProducts.Add(vegetable);
            productsUpdated.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Vegetable>(out var vegetable))
        {
            trackedProducts.Remove(vegetable);
            productsUpdated.Invoke();
        }
    }
}
