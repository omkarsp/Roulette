using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] BetSelection betSelection;
    [SerializeField] Clickable clickable;
    public void StartCountdown(double seconds)
    {
        StartCoroutine(UpdateTimer(seconds));
    }

    IEnumerator UpdateTimer(double seconds)
    {
        bool isNmbTrigger = false;

        WaitForSeconds updateFrequency = new WaitForSeconds(1f);
        int totalTime = Convert.ToInt32(seconds);

        while (totalTime >= 0)
        {
            timerText.text = (totalTime / 60).ToString("00") + ":" + (totalTime % 60).ToString("00");
            yield return updateFrequency;
            totalTime--;
            if (totalTime <= 10 && !isNmbTrigger)
            {
                betSelection.nmbTriggered();//stop betting when 10 seconds are remaining in countdown.
                isNmbTrigger = true;
                clickable.isBettingActive = false;
            }
        }
    }
}
