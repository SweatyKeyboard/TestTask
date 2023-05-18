using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemsCollection _allItemsIds;


    [SerializeField] private Button _deleteButton;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Image _itemIcon;

    [SerializeField] private Sprite _emptySlotSprite;

    private ItemData _item;
    private int _itemCount;

    private bool _isReadyToDelete;
    private bool IsReadyToDelete
    {
        get => _isReadyToDelete;
        set
        {
            _isReadyToDelete = value;
            _deleteButton.gameObject.SetActive(value);
        }
    }


    public ItemData Item => _item;
    public int ItemCount
    {
        get => _itemCount;
        private set
        {
            _itemCount = value;
            _countText.gameObject.SetActive(_item?.IsStackable ?? false && value > 1);
            _countText.text = value.ToString();
        }
    }

    public void ItemClick()
    {
        if (_item == null)
            return;

        IsReadyToDelete = !IsReadyToDelete;
    }

    public void DeleteItem()
    {
        IsReadyToDelete = false;
        _item = null;
        _itemIcon.sprite = _emptySlotSprite;
        ItemCount = 0;
    }

    public void PutItem(ItemData item, int count = 1)
    {
        _item = item;
        ItemCount = count;
        _itemIcon.sprite = item.Icon;
    }

    public void PutItemById(int itemId, int count = 1)
    {

        try
        {
            _item = _allItemsIds.Items.Where(x => x.Id == itemId).ToArray()[0];
            ItemCount = count;
            _itemIcon.sprite = _item.Icon;
        }
        catch
        {

        }
    }

    public void AddItem(int count = 1)
    {
        ItemCount += count;
    }
}
