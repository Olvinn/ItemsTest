using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    // Selected items
    public Item selectedHat { get; private set; }
    public Item selectedBody { get; private set; }
    public Item selectedSkin { get; private set; }

    /// <summary>
    /// All available items
    /// </summary>
    public Dictionary<Id, Item> availableItems { get; private set; }

    /// <summary>
    /// All unavailable items, it should be bought first
    /// </summary>
    public Dictionary<Id, Item> unavailableItems { get; private set; }

    public InventoryEvent onAddAvailableItem, onAddUnavailableItem, onBuyItem, onSelectItem;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        availableItems = new Dictionary<Id, Item>();
        unavailableItems = new Dictionary<Id, Item>();

        onAddAvailableItem = new InventoryEvent();
        onAddUnavailableItem = new InventoryEvent();
        onBuyItem = new InventoryEvent();
        onSelectItem = new InventoryEvent();
    }

    public void AddItemAsAvailable(Item item)
    {
        if (!item)
            return;

        if (!availableItems.ContainsKey(item.id) && !unavailableItems.ContainsKey(item.id))
            availableItems.Add(item.id, item);

        onAddAvailableItem?.Invoke(item);
    }

    public void AddItemAsUnavailable(Item item)
    {
        if (!item)
            return;

        if (!unavailableItems.ContainsKey(item.id) && !availableItems.ContainsKey(item.id))
            unavailableItems.Add(item.id, item);

        onAddUnavailableItem?.Invoke(item);
    }

    public void SetItemAsAvailable(Item item)
    {
        if (!availableItems.ContainsKey(item.id) && unavailableItems.ContainsKey(item.id))
        {
            availableItems.Add(item.id, item);
            unavailableItems.Remove(item.id);
        }

        onBuyItem?.Invoke(item);
    }

    public void SelectItem(Item item)
    {
        if (!item)
            return;

        if (!availableItems.ContainsKey(item.id))
            return;

        switch (item.id.type)
        {
            case ItemType.Body:
                selectedBody = item;
                break;
            case ItemType.Hat:
                selectedHat = item;
                break;
            case ItemType.Skin:
                selectedSkin = item;
                break;
        }

        onSelectItem?.Invoke(item);
    }
}
