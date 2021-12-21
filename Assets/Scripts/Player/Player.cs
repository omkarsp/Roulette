using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerBalanceText;
    [SerializeField] private TextMeshProUGUI playerProfitText;
    [SerializeField] private TextMeshProUGUI playerName;
    public double playerBalance = 50000;
    public int profit;
    public string id = "1";
    public string password = "abcd1234";

    //private void Awake()
    //{
    //    Init();
    //}

    private void Init()
    {
        playerBalanceText.text = playerBalance.ToString();
    }

    public void UpdateBalance(double _netProfit)
    {
        playerBalance += _netProfit;
        playerBalanceText.text = playerBalance.ToString();
    }

    //When coins are put on table
    public void BetMoney(double _betAmount)
    {
        playerBalance -= _betAmount;
        playerBalanceText.text = playerBalance.ToString();
    }

    public void SetPlayerName(string _member)
    {
        playerName.text = _member;
    }

    public void SetPlayerBalance(double _balance)
    {
        playerBalance = _balance;
        playerBalanceText.text = _balance.ToString();
    }

    public void SetPlayerProfit(double _profit)
    {
        playerProfitText.text = _profit.ToString();
    }
}
