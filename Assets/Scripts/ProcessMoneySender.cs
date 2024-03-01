using GameCreator.Runtime.Inventory;
using System;
using UnityEngine;

public class ProcessMoneySender : Singleton<ProcessMoneySender>
{
    [SerializeField] private string _requestType;
    [SerializeField] private Bag _bag;
    [SerializeField] private Currency _currency;

    public void SendProcess(GameObject moneyObject, string moneySourceName)
    {
        ServerSender.Instance.Send(new ProcessMoneyRequest(PlayerProfileController.Instance.PlayerIdentity, moneySourceName), _requestType, (response) => OnReceive(moneyObject, response));
    }

    private void OnReceive(GameObject moneyObject, ResponseObject response)
    {
        Destroy(moneyObject);
        var processMoney = ServerSender.ConvertFromJSON<ProcessMoneyResponse>(response.Body);
        _bag.Wealth.Set(_currency, (int)processMoney.PlayerProfileTable.MoneyAmount);
    }

    [Serializable]
    public class ProcessMoneyRequest
    {
        public PlayerIdentity PlayerIdentity;
        public string MoneySource;

        public ProcessMoneyRequest(PlayerIdentity playerIdentity, string moneySource)
        {
            PlayerIdentity = playerIdentity;
            MoneySource = moneySource;
        }
    }

    [Serializable]
    public class ProcessMoneyResponse
    {
        public PlayerProfileTable PlayerProfileTable;

        public ProcessMoneyResponse(PlayerProfileTable playerProfileTable)
        {
            PlayerProfileTable = playerProfileTable;
        }
    }
}
