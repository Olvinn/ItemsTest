using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemsRawWidget : MonoBehaviour
{
    [SerializeField] ItemButton[] cells;

    public InventoryEvent onBuy, onSelect;

    private void Awake()
    {
        onBuy = new InventoryEvent();
        onSelect = new InventoryEvent();
    }

    private void Start()
    {
        UpdateVisuals();

        foreach (ItemButton cell in cells)
        {
            cell.onBuy.AddListener((Item item) => onBuy?.Invoke(item));
            cell.onSelect.AddListener((Item item) => onSelect?.Invoke(item));
        }
    }

    public void SetItem(int pos, Item item, bool bought)
    {
        if (pos < 0 || pos >= 5)
            return;
        cells[pos].item = item;
        cells[pos].bought = bought;
    }

    public void UpdateVisuals()
    {
        foreach (ItemButton cell in cells)
        {
            cell.UpdateVisuals();
        }
    }

    public void Clear()
    {
        foreach (ItemButton cell in cells)
        {
            cell.item = null;
            cell.UpdateVisuals();
        }
    }
}
