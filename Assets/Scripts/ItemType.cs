using GameCreator.Runtime.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemType", menuName = "ItemType", order = 1)]
public class ItemType : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Item _item;

    public string Name => _name;
    public Item Item => _item;
}
