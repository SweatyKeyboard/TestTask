using System;
using UnityEngine;

[Serializable]
public class DropItem
{
    [SerializeField] private ItemData _item;
    [SerializeField] private int _minCount;
    [SerializeField] private int _maxCount;
    [SerializeField] private int _dropWeight;

    public ItemData Item => _item;
    public int MinCount => _minCount;
    public int MaxCount => _maxCount;
    public int DropWeight => _dropWeight;
}