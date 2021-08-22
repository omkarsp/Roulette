using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentBetsElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI betType;
    [SerializeField] TextMeshProUGUI betAmount;

    public void FillCurrentBetElement(BettingDetails bettingDetails)
    {
        playerName.text = bettingDetails.Member;
        betType.text = bettingDetails.Type;
        betAmount.text = bettingDetails.Amount.ToString();
    }
}
