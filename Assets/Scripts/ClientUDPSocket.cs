using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ClientUDPSocket : MonoBehaviour
{
    [SerializeField] private string serverIP = "127.0.0.1";
    [SerializeField] private int serverPort;
    
    private UdpClient udpClient;
    private IPEndPoint serverEndPoint;

    public event Action<byte[]> OnReceiveCallback;

    private void Start()
    {
        udpClient = new UdpClient();
        serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

        udpClient.BeginReceive(ReceiveCallback, null);
    }

    private void OnDestroy()
    {
        udpClient.Close();
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            byte[] data = udpClient.EndReceive(ar, ref serverEndPoint);

            OnReceiveCallback?.Invoke(data);

        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving UDP message: " + e.Message);
        }

        udpClient.BeginReceive(ReceiveCallback, null);
    }

    public void SendMessageToServer(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            Debug.Log(message);
            udpClient.Send(data, data.Length, serverEndPoint);
        }
        catch (Exception e)
        {
            Debug.LogError("Error sending UDP message: " + e.Message);
        }
    }
}
