using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfileController : Singleton<PlayerProfileController>
{
    [SerializeField] private string _authentificationRequestType;
    [SerializeField] private string _getInventoryType;
    [SerializeField] private PopupDisplay _banPopup;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private int _mainGameBuildIndex;

    [SerializeField] private PlayerProfileTable _profileTable;
    
    public PlayerProfileTable ProfileTable => _profileTable;

    public PlayerIdentity PlayerIdentity => new PlayerIdentity(_profileTable.Nickname);

    public List<string> InventoryItems;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ShowFailMessage(string message)
    {
        _banPopup.ChangeMessage(message);
        _banPopup.Enable();
    }

    public void Send()
    {
        string login = _inputField.text;
        var serverSender = ServerSender.Instance;
        serverSender.Send(new AuthentificationRequest(new PlayerIdentity(login)), _authentificationRequestType, OnAuthenticationReceived);
    }

    private void OnAuthenticationReceived(ResponseObject response)
    {
        var authentificationConfirmation = ServerSender.ConvertFromJSON<AuthentificationConfirmation>(response.Body);
        if (authentificationConfirmation.Permitted)
        {
            _profileTable = authentificationConfirmation.PlayerProfile;
            SyncInventory();
        }
        else
        {
            _banPopup.Enable();
        }
    }

    private void SyncInventory()
    {
        ServerSender.Instance.Send(new AuthentificationRequest(PlayerIdentity), _getInventoryType, OnSyncInventory);
    }

    private void OnSyncInventory(ResponseObject response)
    {
        var inventoryData = ServerSender.ConvertFromJSON<InventoryData>(response.Body);
        InventoryItems = inventoryData.Items;
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_mainGameBuildIndex);
    }

    [Serializable]
    private class AuthentificationRequest
    {
        public PlayerIdentity PlayerIdentity;

        public AuthentificationRequest(PlayerIdentity playerIdentity)
        {
            PlayerIdentity = playerIdentity;
        }
    }

    [Serializable]
    private class AuthentificationConfirmation
    {
        public bool Permitted;

        public string Message;

        public PlayerProfileTable PlayerProfile;

        public AuthentificationConfirmation(bool permitted, string message, PlayerProfileTable profileTable)
        {
            Permitted = permitted;
            Message = message;
            PlayerProfile = profileTable;
        }
    }

    [Serializable]
    private class InventoryData
    {
        public List<String> Items;
    }
}
