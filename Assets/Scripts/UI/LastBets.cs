using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

public class LastBets : MonoBehaviour
{
    [SerializeField] private Transform betsViewParent;
    [SerializeField] private GameObject betsViewElement;
    [SerializeField] private int noOfBets = 5;
    private List<PreviousWinnerList> lastBetsList;

    public void UpdateBetList(List<PreviousWinnerList> list)
    {
        lastBetsList = list;

        FillLastBetsView();
    }

    public void FillLastBetsView()
    {
        if (lastBetsList != null)
        {
            for (int i = 0; i < lastBetsList.Count; i++)
            {
                GameObject go = Instantiate(betsViewElement, betsViewParent);
                go.GetComponent<TextMeshProUGUI>().text = lastBetsList[i].Gametype.ToString();
            }
        }
    }
}
