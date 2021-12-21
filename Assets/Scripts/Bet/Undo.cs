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

    public void UpdateCoinList(GameObject _coinToPush)
    {
        _coinToPush.GetComponent<CoinOnTable>().coinValue = currentChip.value;
        coinValues.Push(_coinToPush.GetComponent<CoinOnTable>().coinValue);
        coinsInGame.Push(_coinToPush);
    }

    public void UndoLastBet()
    {
        if (coinsInGame.Count > 0)
        {
            Debug.Log("stack count 1: " + coinsInGame.Count);
            bet.UpdateBetAmount(-coinsInGame.Peek().GetComponent<CoinOnTable>().coinValue);
            Debug.Log("stack count 2: " + coinsInGame.Count);
            Destroy(coinsInGame.Pop());
            Debug.Log("stack count 3: " + coinsInGame.Count);
            //coinsInGame.Pop();
            //Debug.Log("stack count 4: " + coinsInGame.Count);
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
        coinValues.Clear();

        //for (int i = 0; i < coinsInGame.Count; i++)
        //{
        //    Destroy(coinsInGame.Pop());
        //}

        foreach (GameObject coin in coinsInGame) Destroy(coin);
    }

    private void OnDestroy()
    {
        undoButton.onClick.RemoveAllListeners();
    }
}
