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

    public void UpdateBalance(double netProfit)
    {
        playerBalance += netProfit;
        playerBalanceText.text = playerBalance.ToString();
    }

    //When coins are put on table
    public void BetMoney(double betAmount)
    {
        playerBalance -= betAmount;
        playerBalanceText.text = playerBalance.ToString();
    }

    public void SetPlayerName(string member)
    {
        playerName.text = member;
    }

    public void SetPlayerBalance(double balance)
    {
        playerBalance = balance;
        playerBalanceText.text = balance.ToString();
    }

    public void SetPlayerProfit(double profit)
    {
        playerProfitText.text = profit.ToString();
    }
}
