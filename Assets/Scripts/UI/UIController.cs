using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] LastBets lastBets;
    [SerializeField] private TextMeshProUGUI gameId;
    [SerializeField] private Countdown countdown;

    //Currently not using this because data is changed to json format.
    public void PopulateData(string member, double gameBalance, double winBalance, string count, string list, int gameId, double timer)
    {
        player.SetPlayerName(member);
        player.SetPlayerBalance(gameBalance);
        player.SetPlayerProfit(winBalance);
        countdown.StartCountdown(timer, gameId);
        //lastBets.UpdateBetList(list);
        this.gameId.text = "GAME ID: " + gameId.ToString();
    }

    public void PopulateDataJson(OnConnected data)
    {
        if (data.Userdetails != null)
        {
            Debug.Log("Member ID/Username: " + data.Userdetails.Memberid);
            player.SetPlayerName(data.Userdetails.Memberid);

            Debug.Log("Account balance: " + data.Userdetails.AccountBalance);
            player.SetPlayerBalance(data.Userdetails.AccountBalance);

            Debug.Log("Game balance: " + data.Userdetails.GameBalance);
            player.SetPlayerProfit(data.Userdetails.GameBalance);

            Debug.Log("Authentication: " + data.Userdetails.Authentication);
            Debug.Log("Connection ID: " + data.Userdetails.ConnectionId);
            Debug.Log("M ID: " + data.Userdetails.Mid);
        }

        Debug.Log("Game timer duration: " + data.GameTimer.timer);
        countdown.StartCountdown(data.GameTimer.timer, data.GameTimer.gameid);

        Debug.Log("Game ID: " + data.GameTimer.gameid);
        gameId.text = "GAME ID: " + data.GameTimer.gameid.ToString();

        foreach (PreviousWinnerList item in data.PreviousWinnerList)
        {
            Debug.Log(item.Gametype.ToString());
        }

        lastBets.UpdateBetList(data.PreviousWinnerList);
    }
}
