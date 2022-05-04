using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TradeTableUIUpdater : MonoBehaviour
{
    [SerializeField] private ProductTracker productTable;
    [SerializeField] private TMP_Text productCountText;

    private void Awake()
    {
        productTable.productsUpdated.AddListener(UpdateProducts);
    }

    private void OnDisable()
    {
        productTable.productsUpdated.RemoveListener(UpdateProducts);
    }

    private void UpdateProducts()
    {
        productCountText.SetText($"Products On Table: {productTable.trackedProducts.Count}");
    }
}
