using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isStackable;

    public int Id => _id;
    public string Name => _name;
    public Sprite Icon => _icon;
    public bool IsStackable => _isStackable;
}
