using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    [SerializeField] float wheelSpeed = 10f;
    private bool isRotate = false;

    public void Init()
    {
        //isRotate = true;
    }

    private void Update()
    {
        if (isRotate) RotateWheel();
    }

    private void RotateWheel()
    {
        transform.Rotate(0, -wheelSpeed * Time.deltaTime, 0);
    }

    public void WheelSpinToggle()
    {
        //isRotate = !isRotate;
        if(!isRotate) isRotate = true;
    }
}
