using UnityEngine;
using Microsoft.AspNet.SignalR.Client;

public class DisconnectionHandler : MonoBehaviour
{
    [SerializeField] private SignalRConnection signalRConnection;
    public ConnectionState connectionState;

    public void Init()
    {

    }
}
