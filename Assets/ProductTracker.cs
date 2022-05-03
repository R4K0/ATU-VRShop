using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTracker : MonoBehaviour
{
    [SerializeField] private List<Vegetable> trackedProducts;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Vegetable>(out var vegetable) && !trackedProducts.Contains(vegetable))
        {
            trackedProducts.Add(vegetable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Vegetable>(out var vegetable))
        {
            trackedProducts.Remove(vegetable);
        }
    }
}
