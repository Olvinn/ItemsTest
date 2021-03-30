using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory instance;

    [SerializeField] Item[] items;

    Dictionary<Id, Item> _items;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        _items = new Dictionary<Id, Item>();

        foreach (Item item in items)
        {
            _items.Add(item.id, item);
        }
    }

    public Item GetItem(Id id)
    {
        Item result = ScriptableObject.CreateInstance(typeof(Item)) as Item;
        result.Init(_items[id]);
        return result;
    }

    public List<Item> GetAllItems()
    {
        List<Item> result = new List<Item>(); 
        foreach (Id id in _items.Keys)
        {
            result.Add(GetItem(id));
        }
        return result;
    }
}
