using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SignalRConnection : MonoBehaviour
{
    public Action<double> OnDataReceived;
    private static HubConnection hubConnection;
    private static IHubProxy hubProxy;
    [SerializeField] private Player player;
    public TextMeshProUGUI balanceTxt;
    double balance = 111;
    [SerializeField] GameManager gameManager;
    [SerializeField] Clickable clickable;
    [SerializeField] BetSelection betSelection;

    #region Received Data
    public string member;
    public double gamebalance;
    public double winbalance;
    public string count;
    public string list;
    public int gameid;
    public double timer;
    public string transaction;
    #endregion

    public void Init()
    {
        StartAsync();
    }

    public void DisplayData()
    {
        balanceTxt.text = balance.ToString();
    }

    public async Task StartAsync()
    {
        hubConnection = new HubConnection("http://www.officewrk.online/signalr");
        hubProxy = hubConnection.CreateHubProxy("rouletteHub");

        hubConnection.Closed += async () =>
        {
            await hubConnection.Start();
        };

        try
        {
            await hubConnection.Start();
            Debug.Log("Connected");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        Connect();//output
        Send("Game");//input
    }

    private async void Connect()
    {
        try
        {
            hubProxy.On<string, double, double, string, string, int, double>("onConnected", (member, gamebalance, winbalance, count, list, gameid, timer) =>
            {
                Debug.Log(member + "|\n" + gamebalance + "|" + winbalance + "|" + count + "|" + list.GetType() + " : " + list + "|" + gameid + "|" + timer);

                this.member = member;
                this.gamebalance = gamebalance;
                this.winbalance = winbalance;
                this.count = count;
                this.list = list;
                this.gameid = gameid;
                this.timer = timer;

                Dispatcher.Instance.Dispatch(MapReceivedData);
            });

            hubProxy.On<string, string>("RoulleteTransaction", (member, transaction) =>
            {
                Debug.Log(member + "|\n" + transaction);

                this.member = member;
                this.transaction = transaction;

                Dispatcher.Instance.Dispatch(MapTransactionData);
            });
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

        try
        {
            await hubConnection.Start();

            Debug.Log("Connection started");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private async void Send(string member)
    {
        try
        {
            await hubProxy.Invoke("onstart", member, "sdasdsad");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        try
        {
            await hubConnection.Start();

            Debug.Log("Connection started");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public async void Betting(RouletteBettings betting)
    {
        Debug.Log("test betting");
        try
        {
            await hubProxy.Invoke("onbetting", betting);
        }
        
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        
        try
        {
            await hubConnection.Start();

            Debug.Log("Connection started");
        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void MapReceivedData()
    {
        gameManager.onDataReceived(member, gamebalance, winbalance, count, list, gameid, timer);
        clickable.isBettingActive = true;
    }

    private void MapTransactionData()
    {
        gameManager.onTransactionData(member, transaction);
    }
}

