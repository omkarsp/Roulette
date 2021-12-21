using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private float ballRotateDuration = 2.5f;
    [SerializeField] private float ballSpiralDuration = 2.5f;
    [SerializeField] private float ballRotateSpeed = 10f;
    [SerializeField] private float ballMoveSpeed = 10f;
    [SerializeField] private Transform targetPos;
    [SerializeField] private Button spinButton;
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 centerPos;
    private Collider targetCollider;
    private bool isRotate = true;

    [Header("Script References")]
    [SerializeField] private Result result;
    [SerializeField] private BallTarget ballTarget;
    [SerializeField] private CameraAnimation cameraAnimation;

    public void Awake()
    {
        centerPos = wheelTransform.position;
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    //On clicking SPIN button.
    public void StartBallSpinRoutine()
    {
        StartCoroutine(BallSpinRoutine());
    }

    IEnumerator BallSpinRoutine()
    {
        spinButton.interactable = false;
        ResetBall();
        float ballSpeed = ballRotateSpeed;
        float duration = ballRotateDuration;

        //Initially ball rotates in circles for few seconds
        while (duration > 0)
        {
            transform.RotateAround(centerPos, Vector3.up, ballSpeed);
            yield return null;
            duration -= Time.deltaTime;
        }

        //duration = ballSpiralDuration;
        rb.isKinematic = false;

        //Ball slows down while rotating naturally
        while (/*duration > 0*//*Vector3.Distance(transform.position, targetPos.position) > 0.01*/isRotate)//isRotate will be set to false when triggers with target
        {
            transform.RotateAround(centerPos, Vector3.up, ballSpeed);
            yield return null;
            ballSpeed = Mathf.Max(2f, ballSpeed * 0.95f);
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

        yield return new WaitForSeconds(1f);

        result.ShowResult();

        yield return new WaitForSeconds(0.5f);

        cameraAnimation.MoveToTabloe();
    }

    //Stop spinning the ball when it hits the target collider
    private void OnTriggerEnter(Collider other)
    {
        if(other == targetCollider) isRotate = false;
    }

    public void ResetBall()
    {
        transform.position = startPos;
        rb.isKinematic = true;
        targetPos = ballTarget.GenerateRandomResult();
        targetCollider = ballTarget.GetCollider();
        rb.freezeRotation = false;
    }
}
