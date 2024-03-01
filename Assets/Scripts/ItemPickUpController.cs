using UnityEngine;

public class ItemPickUpController : MonoBehaviour
{
    public void OnPickUp(string itemName)
    {
        LootPickUpSender.Instance.OnPickUpItem(gameObject, itemName);
    }
}
