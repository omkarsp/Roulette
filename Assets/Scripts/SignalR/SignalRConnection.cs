using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    [SerializeField] Countdown countdown;
    private string authenticationId = "testID";

    #region Received Data
    public string member;
    public string mid;
    public double gamebalance;
    public double winbalance;
    public string count;
    public string list;
    public int gameid;
    public double timer;
    public string transaction;
    #endregion

    public OnConnected onConnectedData = new OnConnected();
    private int resultNumber = 0;
    private GameTimerModel gameTimerModel;
    private List<PreviousWinnerList> previousWinnersData;


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
        hubProxy = hubConnection.CreateHubProxy("chatHub");

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

        Connect();//output//receive
        Send("Game", authenticationId);//input//send
    }

    private async void Connect()
    {
        try
        {
            #region comments
            //Clients.Caller.onConnected(
            //        CurrentUser.Memberid,
            //        CurrentUser.Mid,
            //        CurrentUser.AccountBalance,
            //        CurrentUser.GameBalance,
            //        ConnectedUsers.Count.ToString(),
            //        Lastwin,
            //        ConnectedBetting.Where(e => e.Gameid == GameManager.gameid).ToList(),
            //        winnerlist,
            //        GameManager.gameid,
            //        GameManager.timer);

            //hubProxy.On<string, double, double, string, string, int, double>("onConnected", 
            //    (member, gamebalance, winbalance, count, list, gameid, timer) =>
            //{
            //    Debug.Log(member + "|\n" + gamebalance + "|" + winbalance + "|" + count + "|" + list.GetType() + " : " + list + "|" + gameid + "|" + timer);

            //    this.member = member;
            //    this.gamebalance = gamebalance;
            //    this.winbalance = winbalance;
            //    this.count = count;
            //    this.list = list;
            //    this.gameid = gameid;
            //    this.timer = timer;

            //    Dispatcher.Instance.Dispatch(MapReceivedData);
            //});
            #endregion

            hubProxy.On<string>("onConnected",
                (string jsonData) =>
                {
                    Debug.Log("OnConnected data: " + jsonData);
                    OnConnected data = JsonConvert.DeserializeObject<OnConnected>(jsonData);
                    Debug.Log(data.Userdetails);

                    onConnectedData = data;

                    Dispatcher.Instance.Dispatch(MapReceivedData);
                });

            //get current result number
            hubProxy.On<int>("onresult",
               (int _winningNumber) =>
               {
                   //OnConnected data = JsonConvert.DeserializeObject<OnConnected>(jsonData);
                   Debug.Log("Received Result Number: " + _winningNumber);

                   resultNumber = _winningNumber;

                   Dispatcher.Instance.Dispatch(MapResultNumber);
               });

            hubProxy.On<string>("onreset",
             (string onReset) =>
             {
                 //Before binding onreset data clean previous data/old data.
                 Debug.Log("Reset Data: " + onReset);

                 //gameTimerModel = _gameTimerModel;
                 //previousWinnersData = _previousWinnersData;

                 Dispatcher.Instance.Dispatch(MapResetData);

                 //Create and call a method to use this game timer data from here for reset.
             });

            #region comments
            //betting response




            //hubProxy.On<string, string>("RoulleteTransaction", (member, transaction) =>
            //{
            //    Debug.Log(member + "|\n" + transaction);

            //    this.member = member;
            //    this.transaction = transaction;

            //    Dispatcher.Instance.Dispatch(MapTransactionData);
            //});
            #endregion
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

    private async void Send(string member, string authenticationId)
    {
        try
        {
            await hubProxy.Invoke("Connect", member, authenticationId);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        try
        {
            await hubConnection.Start();

            Debug.Log("Send Connection started");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    //GameResult
    //call this when timer is finished or 0.
    public async void GameResult(int gameId)
    {
        try
        {
            Debug.Log("Result Game ID: " + gameId);
            await hubProxy.Invoke("GameResult", gameId);//current result number
        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        try
        {
            await hubConnection.Start();

            Debug.Log("Game Result Connection started");
        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public async void GameReset(GameTimerModel _gameTimerModel, List<PreviousWinnerList> _previousWinnersData)
    {
        try
        {
            await hubProxy.Invoke("ResetGame");
        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        try
        {
            await hubConnection.Start();

            Debug.Log("Game Reset Connection started");
        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public async void Betting(RouletteBettings betting)
    {
        try
        {
            await hubProxy.Invoke("Bedding", betting);
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
        //gameManager.onDataReceived(member, gamebalance, winbalance, count, list, gameid, timer);
        gameManager.onJsonDataReceived(onConnectedData);
        //gameManager.onDataReceived();
        clickable.isBettingActive = true;
    }

    private void MapResultNumber()
    {
        countdown.setResultNum(resultNumber);
    }

    private void MapTransactionData()
    {
        gameManager.onTransactionData(member, transaction);
    }

    private void MapResetData()
    {
        //gameTimerModel, previousWinnersData

    }
}
