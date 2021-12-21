using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    private const float SLOW_FACTOR = 0.99f;
    private const float ANIMATION_DELAY = 0.5f;
    private const float SHOW_RESULT_DELAY = 1f;

    [Header("Movement")]
    [SerializeField] private Transform wheelTransform;

    [Tooltip("The duration for which ball keeps rotating without slowing down")]
    [SerializeField] private float ballRotateDuration = 2.5f;
    [SerializeField] private float ballSpiralDuration = 2.5f;

    [Tooltip("Ball speed before slowing down")]
    [SerializeField] private float ballRotateSpeed = 10f;
    [SerializeField] private float ballMoveSpeed = 10f;

    [Tooltip("Minimum ball speed during the spiral movement")]
    [SerializeField] private float ballMinSpiralSpeed = 200f;
    [SerializeField] private Button spinButton;
    private Transform targetPos;
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 centerPos;
    private Collider targetCollider;
    private bool isRotate = true;

    [Header("Script References")]
    [SerializeField] private Result result;
    [SerializeField] private BallTarget ballTarget;
    [SerializeField] private CameraAnimation cameraAnimation;
    [SerializeField] private SignalRConnection signalRConnection;

    private GameTimerModel gameTimerModel;
    private List<PreviousWinnerList> previousWinnersData;

    public void Awake()
    {
        centerPos = wheelTransform.position;
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    //On clicking SPIN button.
    public void StartBallSpinRoutine(int _resultNum)
    {
        StartCoroutine(BallSpinRoutine(_resultNum));
    }

    IEnumerator BallSpinRoutine(int _resultNum)
    {
        spinButton.interactable = false;
        ResetBall(_resultNum);
        float ballSpeed = ballRotateSpeed;
        float duration = ballRotateDuration;

        //Initially ball rotates in circles for few seconds
        while (duration > 0)
        {
            transform.RotateAround(centerPos, Vector3.up, ballSpeed * Time.deltaTime);
            yield return null;
            duration -= Time.deltaTime;
        }

        //duration = ballSpiralDuration;
        rb.isKinematic = false;

        //Ball slows down while rotating naturally
        while (/*duration > 0*//*Vector3.Distance(transform.position, targetPos.position) > 0.01*/isRotate)//isRotate will be set to false when triggers with target
        {
            transform.RotateAround(centerPos, Vector3.up, ballSpeed * Time.deltaTime);
            yield return null;
            ballSpeed = Mathf.Max(ballMinSpiralSpeed, ballSpeed * Time.deltaTime * SLOW_FACTOR);
            //duration -= Time.deltaTime;
        }

        //while (Vector3.Distance(transform.position, targetPos.position) > 0.01)
        //{
        //    transform.position += (targetPos.position - transform.position).normalized * ballMoveSpeed * Time.deltaTime;
        //    yield return null;
        //}

        transform.position = targetPos.position;
        rb.freezeRotation = true;
        isRotate = true;

        yield return new WaitForSeconds(SHOW_RESULT_DELAY);

        result.ShowResult();

        yield return new WaitForSeconds(ANIMATION_DELAY);

        cameraAnimation.MoveToTable();

        yield return new WaitForSeconds(10);

        //Call to reset the current game 10 to 12 seconds after the spin stops.
        signalRConnection.GameReset(gameTimerModel, previousWinnersData);
    }

    //Stop spinning the ball when it hits the target collider
    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider) isRotate = false;
    }

    public void ResetBall(int _resultNum)
    {
        transform.position = startPos;
        rb.isKinematic = true;
        //targetPos = ballTarget.GenerateRandomResult();
        targetPos = ballTarget.GetResultTransform(_resultNum);
        targetCollider = ballTarget.GetCollider();
        rb.freezeRotation = false;
    }

    public void SetGameResetData(OnConnected _onConnectedData)
    {
        gameTimerModel = _onConnectedData.GameTimer;
        previousWinnersData = _onConnectedData.PreviousWinnerList;
    }
}
