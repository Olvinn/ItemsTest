using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CasinoWidget : MonoBehaviour
{
    public static CasinoWidget instance;

    [SerializeField] Button startButton;
    [SerializeField] Widget reward;
    [SerializeField] RewardWidget rewardVis;
    [SerializeField] TextMeshProUGUI spins;

    public UnityEvent startSpinning;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        startSpinning = new UnityEvent();
    }

    private void Start()
    {
        Casino.instance.onStopSpinning.AddListener(OnStopSpinnig);
        spins.text = Casino.instance.spins.ToString();
    }

    public void StartSpinnig()
    {
        startButton.interactable = false;
        startSpinning?.Invoke();
        spins.text = Casino.instance.spins.ToString();
    }

    public void OnStopSpinnig()
    {
        rewardVis.SetItem(Casino.instance.reward);
        reward.Show();
    }
}
