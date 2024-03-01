using System;
using System.Collections.Generic;
using UnityEngine;

public class ServerSender : Singleton<ServerSender>
{
    [SerializeField] private ClientUDPSocket _clientUDP;

    private Dictionary<RequestObject, Action<ResponseObject>> _requestsCallbacks = new ();

    private List<Action> _callbacksInUpdate = new List<Action>();

    private void OnEnable()
    {
        _clientUDP.OnReceiveCallback += ReceiveServerResponse;
    }
    
    private void OnDisable()
    {
        _clientUDP.OnReceiveCallback -= ReceiveServerResponse;
    }

    private void Update()
    {
        foreach (var callback in _callbacksInUpdate)
        {
            callback.Invoke();
        }
        _callbacksInUpdate.Clear();
    }

    //public void SubscribeToResponse(string responseType, Action<ResponseObject> callback)
    //{
    //    if (!_responseListeners.ContainsKey(responseType))
    //    {
    //        _responseListeners[responseType] = new List<Action<ResponseObject>>();
    //    }

    //    _responseListeners[responseType].Add(callback);
    //}

    private void SendString(string message)
    {
        _clientUDP.SendMessageToServer(message);
    }

    public void Send(object body, string requestType)
    {
        string bodyJson = JsonUtility.ToJson(body);
        var requestObject = new RequestObject(requestType, bodyJson);
        string jsonObject = JsonUtility.ToJson(requestObject);
        SendString(jsonObject);
    }

    public void Send(object body, string requestType, Action<ResponseObject> callback)
    {
        string bodyJson = JsonUtility.ToJson(body);
        var requestObject = new RequestObject(requestType, bodyJson);
        _requestsCallbacks[requestObject] = callback;
        string jsonObject = JsonUtility.ToJson(requestObject);
        SendString(jsonObject);
    }

    private void ReceiveServerResponse(byte[] data)
    {
        string message = System.Text.Encoding.UTF8.GetString(data);
        var responseObject = JsonUtility.FromJson<ResponseObject>(message);

        _callbacksInUpdate.Add(() => _requestsCallbacks[responseObject.Request].Invoke(responseObject));
    }

    public static T ConvertFromJSON<T>(string json) => JsonUtility.FromJson<T>(json);
}
