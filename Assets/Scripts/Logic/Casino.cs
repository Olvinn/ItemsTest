using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Casino : MonoBehaviour
{
    public static Casino instance;

    public Item reward { get; private set; }
    public Queue<Item> lastRewards { get; private set; }
    public UnityEvent onStopSpinning;
    public int spins { get; private set; } = 5;
    [SerializeField] CasinoWheelSegment[] segments;
    [SerializeField] PhysicalWheel physics;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);

        lastRewards = new Queue<Item>();
        onStopSpinning = new UnityEvent();
        lastRewards.Enqueue(null);
        lastRewards.Enqueue(null);
        lastRewards.Enqueue(null);
        lastRewards.Enqueue(null);
    }

    void Start()
    {
        physics.onEnd.AddListener(() => 
        { 
            reward = GetItemBySegment(physics.segment);
            if (reward)
                AddItemTolastRewards(reward);
            onStopSpinning?.Invoke();
        });
    }

    public void StartSpin()
    {
        if (spins > 0)
        {
            spins--;
            physics.Spin();
        }
    }

    public Item GetItemBySegment(string segmentName)
    {
        foreach (CasinoWheelSegment segment in segments)
            if (segment.name == segmentName)
                return segment.item;
        return null;
    }

    public void AddItemTolastRewards(Item item)
    {
        if (!lastRewards.Contains(item))
            lastRewards.Enqueue(item);
        if (lastRewards.Count > 4)
            lastRewards.Dequeue();
    }
}

[Serializable]
public struct CasinoWheelSegment
{
    public string name;
    public Item item;
}
