using GameCreator.Runtime.Characters;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionSender : MonoBehaviour
{
    [SerializeField] private string _requestType;

    public void OnAction(string actionName)
    {
        ServerSender.Instance.Send(new ActionRequest(PlayerProfileController.Instance.PlayerIdentity, actionName), _requestType, OnReceive);
    }

    private void OnReceive(ResponseObject response)
    {
        if (response.CodeStatus == 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    [Serializable]
    private class ActionRequest
    {
        public PlayerIdentity PlayerIdentity;
        public string ActionName;

        public ActionRequest(PlayerIdentity playerIdentity, string actionName)
        {
            PlayerIdentity = playerIdentity;
            ActionName = actionName;
        }
    }
}
