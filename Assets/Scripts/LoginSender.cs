using TMPro;
using UnityEngine;

public class LoginSender : MonoBehaviour
{
    [SerializeField] private string _requestType;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private ServerAuthorizationController _serverAuthorizationController;

    public void OnLogin()
    {
        string login = _inputField.text;
        var playerAuthentication = new PlayerAuthentication(login);

        // _serverAuthorizationController.SendAuthorization(playerAuthentication);
    }
}
