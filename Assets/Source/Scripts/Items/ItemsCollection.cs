
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemsCollection")]
public class ItemsCollection : ScriptableObject
{
    [SerializeField] private ItemData[] _items;

    public ItemData[] Items => _items;
}
