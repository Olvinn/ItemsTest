using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour
{
    void Start()
    {
        foreach (Item item in ItemFactory.instance.GetAllItems())
            if (item.id.number != 1)
                Inventory.instance.AddItemAsUnavailable(item);
            else
            {
                Inventory.instance.AddItemAsAvailable(item);
                Inventory.instance.SelectItem(item);
            }

        Wallet.instance.AddMoney(200);

        InventoryWidget.instance.onBuy.AddListener(BuyItem);
        InventoryWidget.instance.onSelect.AddListener(SelectItem);

        CasinoWidget.instance.startSpinning.AddListener(PlayCasino);
        Casino.instance.onStopSpinning.AddListener(() => GetReward(Casino.instance.reward));
    }

    void BuyItem(Item item)
    {
        if (Wallet.instance.CanBuy(item.price))
        {
            Inventory.instance.SetItemAsAvailable(item);
            Wallet.instance.RemoveMoney(item.price);
        }
    }

    void SelectItem(Item item)
    {
        Inventory.instance.SelectItem(item);
    }

    void PlayCasino()
    {
        Casino.instance.StartSpin();
    }

    void GetReward(Item item)
    {
        switch(item.id.type)
        {
            case ItemType.Money:
                Wallet.instance.AddMoney(item.price);
                break;
            default:
                if (Inventory.instance.unavailableItems.ContainsKey(item.id))
                    Inventory.instance.SetItemAsAvailable(item);
                else
                    Wallet.instance.AddMoney(200);
                break;
        }
    }
}
