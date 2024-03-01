using GameCreator.Runtime.Inventory;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    [SerializeField] private List<ItemType> _itemTypes;
    [SerializeField] private Bag _bag;
    [SerializeField] private Currency _currency;

    private void Start()
    {
        List<string> items = PlayerProfileController.Instance.InventoryItems;
        foreach (string item in items)
        {
            _bag.Content.AddType(GetItemType(item).Item, false);
        }
        _bag.Wealth.Set(_currency, (int)PlayerProfileController.Instance.ProfileTable.MoneyAmount);
    }

    private ItemType GetItemType(string itemName)
    {
        foreach (var itemType in _itemTypes)
        {
            if (itemType.Name == itemName)
            {
                return itemType;
            }
        }
        return null;
    }
}
