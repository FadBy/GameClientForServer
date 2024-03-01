using System;
using UnityEngine;

public class ServerAuthorizationController : MonoBehaviour
{
    [SerializeField] private string _authorizationRequestType;
    
    private bool _authorized;

    private PlayerAuthentication _authorizedPlayer;

    public bool Authorized => _authorized;

    public PlayerAuthentication AuthorizedPlayer => _authorizedPlayer;

    public void Unauthorize()
    {
        if (!_authorized)
            return;
        _authorized = false;
        _authorizedPlayer = null;
    }
    
    public void SendAuthorization(PlayerAuthentication authorizedPlayer, Action<bool, string> callback)
    {
        if (Authorized)
            return;
        var serverSender = ServerSender.Instance;
        
        _authorizedPlayer = authorizedPlayer;
        _authorized = true;
        serverSender.Send(null, _authorizationRequestType, (response) =>
        {
            //var confirmation = JsonUtility.FromJson<AuthorizationConfirmation>(response.Data);
            //OnAuthorizationConfirmed(confirmation.Permitted);
            //callback(confirmation.Permitted, confirmation.Message);
        });
        _authorized = false;
        _authorizedPlayer = null;
    }
    
    private void OnAuthorizationConfirmed(bool permitted)
    {
        if (permitted)
        {
            _authorized = true;
        }
    }
    
    [Serializable]
    public class AuthorizationConfirmation
    {
        [SerializeField]
        private bool _permitted;

        [SerializeField]
        private string _message;

        [SerializeField]
        private PlayerProfileTable _profileTable;

        public string Message => _message;
        
        public bool Permitted => _permitted;

        public PlayerProfileTable ProfileTable => _profileTable;

        public AuthorizationConfirmation(bool permitted, string message, PlayerProfileTable profileTable)
        {
            _permitted = permitted;
            _message = message;
            _profileTable = profileTable;
        }
    }
}
