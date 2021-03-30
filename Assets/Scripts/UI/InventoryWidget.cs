using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryWidget : MonoBehaviour
{
    public static InventoryWidget instance;

    public InventoryEvent onBuy, onSelect;

    [SerializeField] ItemsRawWidget[] raws;

    Item[,] inventory;

    ItemType filter;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        onBuy = new InventoryEvent();
        onSelect = new InventoryEvent();

        inventory = new Item[5, 6];

        Inventory.instance.onAddAvailableItem.AddListener(AddAvailableItem);
        Inventory.instance.onAddUnavailableItem.AddListener(AddUnavailableItem);
        Inventory.instance.onBuyItem.AddListener(BuyItem);
    }

    private void Start()
    {
        foreach (ItemsRawWidget raw in raws)
        {
            raw.onBuy.AddListener((Item item) => onBuy?.Invoke(item));
            raw.onSelect.AddListener((Item item) => onSelect?.Invoke(item));
        }

        ShowHats();
    }

    public void AddAvailableItem(Item item)
    {
        if (item.id.type == filter)
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                for (int i = 0; i < inventory.GetLength(0); i++)
                {
                    if (inventory[j, i] == null)
                    {
                        inventory[j, i] = item;
                        raws[j].SetItem(i, item, true);
                        raws[j].UpdateVisuals();
                        return;
                    }
                }
            }
    }

    public void AddUnavailableItem(Item item)
    {
        if (item.id.type == filter)
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                for (int i = 0; i < inventory.GetLength(0); i++)
                {
                    if (inventory[j, i] == null)
                    {
                        inventory[j, i] = item;
                        raws[j].SetItem(i, item, false);
                        raws[j].UpdateVisuals();
                        return;
                    }
                }
            }
    }

    public void BuyItem(Item item)
    {
        if (item.id.type == filter)
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                for (int i = 0; i < inventory.GetLength(0); i++)
                {
                    if (inventory[j, i].id.Equals(item.id))
                    {
                        inventory[j, i] = item;
                        raws[j].SetItem(i, item, true);
                        raws[j].UpdateVisuals();
                        return;
                    }
                }
            }
    }

    public void ShowHats()
    {
        filter = ItemType.Hat;
        ShowItemsByType(ItemType.Hat);
    }

    public void ShowBodies()
    {
        filter = ItemType.Body;
        ShowItemsByType(ItemType.Body);
    }

    public void ShowSkins()
    {
        filter = ItemType.Skin;
        ShowItemsByType(ItemType.Skin);
    }

    public void ShowItemsByType(ItemType type)
    {
        Clear();

        foreach (Id id in Inventory.instance.availableItems.Keys)
        {
            if (id.type == type)
                AddAvailableItem(Inventory.instance.availableItems[id]);
        }

        foreach (Id id in Inventory.instance.unavailableItems.Keys)
        {
            if (id.type == type)
                AddUnavailableItem(Inventory.instance.unavailableItems[id]);
        }
    }

    public void Clear()
    {
        inventory = new Item[5, 6];

        foreach (ItemsRawWidget raw in raws)
        {
            raw.Clear();
        }
    }
}

public class InventoryEvent : UnityEvent<Item>
{ }
