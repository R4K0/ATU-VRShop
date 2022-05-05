using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TradeTableUIUpdater : MonoBehaviour
{
    [SerializeField] private ProductTracker productTable;
    [SerializeField] private TMP_Text productCountText;
    [SerializeField] private TMP_Text coinsText;

    private void Awake()
    {
        productTable.productsUpdated.AddListener(UpdateProducts);
        productTable.moneyUpdated.AddListener(UpdatePrice);
    }

    private void OnDisable()
    {
        productTable.productsUpdated.RemoveListener(UpdateProducts);
        productTable.moneyUpdated.RemoveListener(UpdatePrice);

    }

    private void UpdatePrice()
    {
        coinsText.SetText($"Coins: {productTable.moneyEarned}");
    }

    private void UpdateProducts()
    {
        productCountText.SetText($"Products On Table: {productTable.trackedProducts.Count}");
    }
}
