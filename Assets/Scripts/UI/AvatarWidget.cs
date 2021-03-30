using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarWidget : MonoBehaviour
{
    public Item hat, body, skin;

    [SerializeField] Image _hatImage, _bodyImage, _skinImage;

    void Start()
    {
        Inventory.instance.onSelectItem.AddListener(SelectItem);
    }

    void Update()
    {
        
    }

    public void SelectItem(Item item)
    {
        switch (item.id.type)
        {
            case ItemType.Body:
                body = item;
                break;
            case ItemType.Hat:
                hat = item;
                break;
            case ItemType.Skin:
                skin = item;
                break;
        }
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (hat != null && hat.id.type == ItemType.Hat)
            _hatImage.sprite = hat.icon;

        if (body != null && body.id.type == ItemType.Body)
            _bodyImage.sprite = body.icon;

        if (skin != null && skin.id.type == ItemType.Skin)
            _skinImage.sprite = skin.icon;
    }
}
