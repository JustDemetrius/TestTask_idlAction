using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StuckModule
{
    [SerializeField] Transform packHolder;
    [SerializeField] int maxCountLength;
    [SerializeField] int maxCountWidth;
    [SerializeField] int maxCountHeight;
    [SerializeField] float _offset = 0.5f;

    public List<Package> _collectedPacks;

    private Vector3 _targetPosition;

    private int _filledCountWidth = 0;
    private int _filledCountLength = 0;
    private int _filledCountHeight = 0;

    public void Init()
    {
        _collectedPacks = new List<Package>();
    }

    public void AddToCarry(Package pack)
    {
        _filledCountWidth = _collectedPacks.Count % maxCountWidth;
        _filledCountLength = _collectedPacks.Count / maxCountWidth % maxCountLength;
        _filledCountHeight = _collectedPacks.Count / (maxCountWidth * maxCountLength);
        _targetPosition = Vector3.right * _filledCountWidth * _offset +
                          Vector3.back * _filledCountLength * _offset +
                          Vector3.up * _filledCountHeight * _offset;

        pack.transform.parent = packHolder;
        pack.transform.forward = packHolder.forward;
        pack.JumpToPlayer(_targetPosition);

        _collectedPacks.Add(pack);
    }

}
