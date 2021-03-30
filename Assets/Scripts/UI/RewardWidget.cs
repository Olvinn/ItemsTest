using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardWidget : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI text;

    public void SetItem(Item item)
    {
        Color temp = bg.color;
        temp.a = 1;
        bg.color = temp;
        icon.enabled = true;
        text.enabled = true;
        icon.sprite = item.icon;
    }
}
