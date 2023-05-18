using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _slots;

    public InventorySlot this[int i] => _slots[i];

    public bool Put(ItemData item, int count)
    {
        int index = -1;

        int i = 0;
        foreach (InventorySlot slot in _slots)
        {
            if (slot.Item == item)
            {
                index = i;
                break;
            }

            i++;
        }

        if (index== -1 || !_slots[index].Item.IsStackable)
        {
            int firstFreeSlot = GetFirstFreeSlot();
            if (firstFreeSlot == -1)
                return false;

            _slots[firstFreeSlot].PutItem(item, count);

            return true;
        }

        _slots[index].AddItem(count);
        return true;
    }

    private int GetFirstFreeSlot()
    {
        int i = 0;
        foreach (InventorySlot slot in _slots)
        {
            if (slot.Item == null)
                return i;
            i++;
        }
        return -1;
    }

}
