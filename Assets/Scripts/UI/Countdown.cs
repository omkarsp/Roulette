using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private BetSelection betSelection;
    [SerializeField] private Clickable clickable;
    [SerializeField] private BallMovement ballMovement;
    [SerializeField] private CameraAnimation cameraAnimation;
    [SerializeField] private SignalRConnection signalRConnection;
    public int gameId = 0;
    public int resultNum = 0;

    public void setResultNum(int _resultNum)
    {
        resultNum = _resultNum;
    }

    public void StartCountdown(double _seconds, int _gameId)
    {
        StartCoroutine(UpdateTimer(_seconds, _gameId));
    }

    IEnumerator UpdateTimer(double _seconds, int _gameId)
    {
        bool _isNmbTrigger = false;
        bool _isResultTriggered = false;

        WaitForSeconds _updateFrequency = new WaitForSeconds(1f);
        int _totalTime = Convert.ToInt32(_seconds);

        while (_totalTime >= 0)
        {
            timerText.text = (_totalTime / 60).ToString("00") + ":" + (_totalTime % 60).ToString("00");
            yield return _updateFrequency;
            _totalTime--;
            if (_totalTime <= 10 && !_isNmbTrigger)
            {
                betSelection.nmbTriggered();//stop betting when 10 seconds are remaining in countdown.
                _isNmbTrigger = true;
                clickable.isBettingActive = false;
            }
            if (_totalTime <= 0 && !_isResultTriggered)
            {
                _isResultTriggered = true;
                signalRConnection.GameResult(_gameId);
            }
        }

        ballMovement.StartBallSpinRoutine(resultNum);
        cameraAnimation.MoveToWheel();
    }
}
