using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class Item : ScriptableObject
{
    public Id id { get { return _id; } }
    public Sprite icon { get { return _icon; } }
    public string itemName { get { return _itemName; } }
    public string description { get { return _description; } }
    public int price { get { return _price; } }

    [SerializeField] Id _id;
    [SerializeField] Sprite _icon;
    [SerializeField] string _itemName;
    [SerializeField] string _description;
    [SerializeField] int _price;

    public void Init(Item re)
    {
        _id = re._id;
        _icon = re._icon;
        _itemName = re._itemName;
        _description = re._description;
        _price = re._price;
    }
}

[Serializable]
public enum ItemType
{
    Hat,
    Body,
    Skin,
    Money
}

[Serializable]
public struct Id
{
    public ItemType type;
    public int number;
}
