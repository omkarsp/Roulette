using System.Collections.Generic;
using UnityEngine;

public class CurrentBets : MonoBehaviour
{
    [SerializeField] GameObject currentBetElement;
    [SerializeField] Transform currentBetsParent;

    public void FillCurrentBets(List<BettingDetails> bettingDetailsList)
    {
        ClearCurrentbets();
        for (int i = 0; i < bettingDetailsList.Count; i++)
        {
            GameObject element = Instantiate(currentBetElement, currentBetsParent);
            element.GetComponent<CurrentBetsElement>().FillCurrentBetElement(bettingDetailsList[i]);
        }
    }

    public void ClearCurrentbets()
    {
        foreach (Transform child in currentBetsParent)
        {
            Destroy(child.gameObject);
        }
    }
}
