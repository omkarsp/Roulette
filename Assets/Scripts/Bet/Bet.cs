using UnityEngine;
using TMPro;

public class Bet : MonoBehaviour
{
    private TextMeshProUGUI betAmountText;
    [SerializeField] private double betAmount = 0;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        betAmountText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBetAmount(double currentBet)
    {
        betAmount += currentBet;
        betAmountText.text = betAmount.ToString();
    }
}
