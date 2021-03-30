using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public static Wallet instance;

    public int amount { get; private set; }

    public UnityEvent onChangeAmount;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);

        onChangeAmount = new UnityEvent();
    }

    public bool CanBuy(int cost)
    {
        if (amount >= cost)
            return true;
        else
            return false;
    }

    public void AddMoney(int amount)
    {
        this.amount += amount;
        onChangeAmount?.Invoke();
    }

    public void RemoveMoney(int amount)
    {
        this.amount -= amount;
        onChangeAmount?.Invoke();
    }
}
