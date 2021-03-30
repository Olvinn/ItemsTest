using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletWidget : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        Wallet.instance.onChangeAmount.AddListener(UpdateVisuals);
    }

    public void UpdateVisuals()
    {
        text.text = Wallet.instance.amount.ToString();
    }
}
