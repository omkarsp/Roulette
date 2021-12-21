using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] LastBets lastBets;
    [SerializeField] private TextMeshProUGUI gameId;
    [SerializeField] private Countdown countdown;

    public void PopulateData(string member, double gameBalance, double winBalance, string count, string list, int gameId, double timer)
    {
        player.SetPlayerName(member);
        player.SetPlayerBalance(gameBalance);
        player.SetPlayerProfit(winBalance);
        countdown.StartCountdown(timer);
        lastBets.UpdateBetList(list);
        this.gameId.text = "GAME ID: " + gameId.ToString();
    }
}
