using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class NoMoreBets : MonoBehaviour
{
    //Write anything which should happen when nmb is triggered in this script.
    public void DisableBetting()
    {

    }

    public void OnNMBData(string member, string transaction)
    {
        List<BettingDetails> bettingDetails = JsonConvert.DeserializeObject<List<BettingDetails>>(transaction);

        Debug.Log("Transaction Details: " + transaction);
        Debug.Log("Member: " + member);
    }
}
