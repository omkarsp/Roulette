﻿using UnityEngine;
using TMPro;
using GameScrolling;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SignalRConnection connection;
    Tester tester;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI playerBalanceTxt;
    public Action<string, double, double, string, string, int, double> onDataReceived;
    public Action<string, string> onTransactionData;
    [SerializeField] UIController uIController;
    [SerializeField] NoMoreBets noMoreBets;

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

    private void Awake()
    {
        //connection = new SignalRConnection();
        //tester = new Tester();
        //connection.StartAsync();
        connection.Init();
        AddListeners();
        //Task.Run(async () =>
        //{
        //    //await connection.StartAsync();
        //    await tester.StartAsync();
        //});
        //tester.StartAsync();
    }

    private void AddListeners()
    {
        //tester.GameWalletReceived += UpdatePlayerBalance;
        //connection.OnDataReceived += UpdatePlayerBalance;
        onDataReceived += uIController.PopulateData;
        onTransactionData += noMoreBets.OnNMBData;
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        //tester.GameIdReceived -= UpdatePlayerBalance;
        //connection.OnDataReceived -= UpdatePlayerBalance;
        onDataReceived -= uIController.PopulateData;
        onTransactionData -= noMoreBets.OnNMBData;
    }
}