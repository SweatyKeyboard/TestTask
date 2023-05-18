using UnityEngine;


public class PlayerToInventory : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public Inventory Inventory => _inventory;
}
