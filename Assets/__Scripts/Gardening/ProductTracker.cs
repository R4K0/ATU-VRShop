using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProductTracker : MonoBehaviour
{
    public List<Vegetable> trackedProducts { private set; get; } = new List<Vegetable>();

    public UnityEvent productsUpdated;

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
