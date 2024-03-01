using GameCreator.Runtime.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUpSender : Singleton<LootPickUpSender>
{
    [SerializeField] private string _requestType;
    [SerializeField] private Bag _bag;
    [SerializeField] List<ItemType> _itemTypes;
    
    public void OnPickUpItem(GameObject itemObject, string itemName)
    {
        foreach (var itemType in _itemTypes)
        {
            if (itemType.Name == itemName)
            {
                if (_bag.Content.CountType(itemType.Item) != 1)
                {
                    ServerSender.Instance.Send(new LootPickUpRequest(PlayerProfileController.Instance.PlayerIdentity, itemName), _requestType, (response) => OnReceiveResponse(itemObject, response));
                }
                else
                {
                    Destroy(itemObject);
                }
                break;
            }
        }
    }

    private void OnReceiveResponse(GameObject itemObject, ResponseObject response)
    {
        Destroy(itemObject);
        var lootPickUpResponce = ServerSender.ConvertFromJSON<LootPickUpResponse>(response.Body);
        string itemName = lootPickUpResponce.ItemName;
        foreach (var itemType in _itemTypes)
        {
            if (itemType.Name == itemName)
            {
                _bag.Content.AddType(itemType.Item, false);
                return;
            }
        }
    }

    [Serializable]
    public class LootPickUpRequest
    {
        public PlayerIdentity PlayerIdentity;
        public string DropSource;

        public LootPickUpRequest(PlayerIdentity playerIdentity, string dropSource)
        {
            PlayerIdentity = playerIdentity;
            DropSource = dropSource;
        }
    }

    [Serializable]
    public class LootPickUpResponse
    {
        public string ItemName;
    }
}
