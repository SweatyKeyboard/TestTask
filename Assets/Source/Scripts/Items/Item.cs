using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private int _count;
    private SpriteRenderer _spriteRenderer;

    public void SetItem(DropItem item)
    {
        _itemData = item.Item;
        _spriteRenderer.sprite = _itemData.Icon;

        if (!item.Item.IsStackable)
            return;
        _count = Random.Range(item.MinCount, item.MaxCount);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _itemData.Icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerToInventory playerToInventory))
        {
            if (playerToInventory.Inventory.Put(_itemData, _count))
                Destroy(gameObject);
        }
    }
}
