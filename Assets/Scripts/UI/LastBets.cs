using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

public class LastBets : MonoBehaviour
{
    [SerializeField] private Transform betsViewParent;
    [SerializeField] private GameObject betsViewElement;
    [SerializeField] private int noOfBets = 5;
    private List<int> lastBetsList;

    public void UpdateBetList(string list)
    {
        lastBetsList = JsonConvert.DeserializeObject<List<int>>(list);

        FillLastBetsView();
    }

    public void FillLastBetsView()
    {
        for (int i = 0; i < noOfBets; i++)
        {
            GameObject go = Instantiate(betsViewElement, betsViewParent);
            go.GetComponent<TextMeshProUGUI>().text = lastBetsList[i].ToString();
        }
    }
}
