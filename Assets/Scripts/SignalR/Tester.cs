using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using UnityEngine;

namespace GameScrolling
{
    public class Tester
    {
        public event Action<double> GameWalletReceived;
        public event Action<double> WinWalletReceived;
        public event Action<double> GameIdReceived;
        public event Action<double> TimerReceived;
        public event Action<string> MemberReceived;
        public event Action<object> LastWiningNumber;
        public event Action<string> TotalUserOnline;
        public event Action<string> DisconnectMember;


        private HubConnection hubConnection;
        private IHubProxy hubProxy;
        [SerializeField] private Dispatcher dispatcher;
        public async Task StartAsync()
        {
            hubConnection = new HubConnection("http://www.officewrk.online/signalr");
            hubProxy = hubConnection.CreateHubProxy("rouletteHub");

            hubConnection.Closed += async () =>
            {
                await hubConnection.Start();
                Debug.Log("Connected");
            };

            try
            {
                await hubConnection.Start();
            }
            catch (Exception ex)
            { }

            Gamestart();//Game Start
            BalanceUpdate();//For Balance Update
            UserDisConnect();//When Any Other User disconnect from Game It Show Memberid Of Disconnected user



            Send("Game");//On Game Start Enter Member or Login ID
            BalanceCheck("Game");//Click here To Refresh Balance In Case Balance Is not Refresh
        }

        private async void Gamestart()
        {
            try
            {
                hubProxy.On<string, double, double, string, object, int, double>("onConnected", (member, gamebalance, winbalance, count, list, gameid, timer) =>
                {
                    Debug.Log(member + "|\n" + gamebalance + "|" + winbalance + "|" + count + "|" + list + "|" + gameid + "|" + timer);

                    GameWalletReceived(gamebalance);
                    WinWalletReceived(winbalance);
                    GameIdReceived(gameid);
                    TimerReceived(timer);
                    MemberReceived(member);
                    LastWiningNumber(list);
                    TotalUserOnline(count);
                });
            }
            catch (Exception ex)
            { }

            try
            {
                await hubConnection.Start();
            }
            catch (System.Exception ex)
            {
            }
        }

        private async void BalanceUpdate() {
            try
            {
                hubProxy.On<string, double, double>("onBalanceUpdate", (member, gamebalance, winbalance) =>
                {
                    GameWalletReceived(gamebalance);
                    WinWalletReceived(winbalance);
                    MemberReceived(member);
                });
            }
            catch (Exception ex)
            { }

            try
            {
                await hubConnection.Start();
            }
            catch (System.Exception ex)
            {
            }
        }
        private async void UserDisConnect()
        {
            try
            {
                hubProxy.On<string, string>("onUserDisconnected", (member,member1) =>
                {
                    DisconnectMember(member);
                });
            }
            catch (Exception ex)
            { }

            try
            {
                await hubConnection.Start();
            }
            catch (System.Exception ex)
            {
            }
        }

        private async void Send(string member)
        {
            try
            {
                await hubProxy.Invoke("onstart", member, "sdasdsad");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                await hubConnection.Start();

                Console.WriteLine("Connection started");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void BalanceCheck(string member)
        {
            try
            {
                await hubProxy.Invoke("onBalance", member, "sdasdsad");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                await hubConnection.Start();

                Console.WriteLine("Connection started");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}