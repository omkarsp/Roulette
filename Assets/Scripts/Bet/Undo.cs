using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Undo : MonoBehaviour
{
    Stack<GameObject> coinsInGame;
    Stack<double> coinValues;
    [SerializeField] Player player;
    [SerializeField] Bet bet;
    [SerializeField] CurrentChip currentChip;
    [SerializeField] BetSelection betSelection;
    Button undoButton;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        coinsInGame = new Stack<GameObject>();
        coinValues = new Stack<double>();
        undoButton = GetComponent<Button>();
        undoButton.onClick.AddListener(UndoLastBet);
    }

    public void UpdateCoinList(GameObject coinToPush)
    {
        coinToPush.GetComponent<CoinOnTable>().coinValue = currentChip.value;
        coinValues.Push(coinToPush.GetComponent<CoinOnTable>().coinValue);
        coinsInGame.Push(coinToPush);
    }

    public void UndoLastBet()
    {
        if (coinsInGame.Count > 0)
        {
            bet.UpdateBetAmount(-coinsInGame.Peek().GetComponent<CoinOnTable>().coinValue);
            Destroy(coinsInGame.Peek());
            coinsInGame.Pop();
        }
        if (coinValues.Count > 0)
        {
            player.UpdateBalance(coinValues.Peek());
            coinValues.Pop();
        }
        if (betSelection.bettingRequests.Count > 0)
        {
            //remove the latest element from the bettype list that is being sent to the server
            betSelection.bettingRequests.RemoveAt(betSelection.bettingRequests.Count - 1);
        }
    }

    public void ClearCoinsFromTable()
    {
        //coinsInGame.Clear();
        coinValues.Clear();
        foreach (GameObject coin in coinsInGame) Destroy(coin);
    }

    private void OnDestroy()
    {
        undoButton.onClick.RemoveAllListeners();
    }
}
