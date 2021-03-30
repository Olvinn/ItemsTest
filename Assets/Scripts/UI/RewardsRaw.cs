using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsRaw : MonoBehaviour
{
    [SerializeField] RewardWidget[] rewards;

    void Start()
    {
        Casino.instance.onStopSpinning.AddListener(() =>
        {
            for (int i = 0; i < rewards.Length; i++)
            {
                Item item = Casino.instance.lastRewards.ToArray()[i];
                if (item)
                    rewards[i].SetItem(item);
            }
        });
    }

}
