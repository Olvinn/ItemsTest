using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Sprite active, inactive;
    [SerializeField] Image buttonImage;
    [SerializeField] Image itemImage;
    [SerializeField] GameObject curtain;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject priceWidget;

    [SerializeField] public Item item;
    [SerializeField] public bool bought;
    public InventoryEvent onBuy, onSelect;

    private void Awake()
    {
        onBuy = new InventoryEvent();
        onSelect = new InventoryEvent();
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ProceedClick);
    }

    public void UpdateVisuals()
    {
        if (item != null)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = item.icon;
            buttonImage.sprite = active;
            if (bought)
            {
                curtain.SetActive(false);
                priceWidget.SetActive(false);
            }
            else
            {
                curtain.SetActive(true);
                priceWidget.SetActive(true);
                priceText.text = item.price.ToString();
            }
        }
        else
        {
            curtain.SetActive(false);
            priceWidget.SetActive(false);
            itemImage.gameObject.SetActive(false);
            buttonImage.sprite = inactive;
        }
    }

    void ProceedClick()
    {
        if (item)
        {
            if (bought)
            {
                onSelect?.Invoke(item);
            }
            else
            {
                onBuy?.Invoke(item);
            }
        }
    }
}
