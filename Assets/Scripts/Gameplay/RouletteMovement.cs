using UnityEngine;
using UnityEngine.UI;

public class RouletteMovement : MonoBehaviour
{
    [SerializeField] BallMovement ballMovement;
    [SerializeField] WheelMovement wheelMovement;

    private void Awake()
    {
        //ballMovement.Init();
        wheelMovement.Init();
    }
}
