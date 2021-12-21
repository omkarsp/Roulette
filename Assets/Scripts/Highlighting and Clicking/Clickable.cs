using UnityEngine;
using System;

public class Clickable : MonoBehaviour
{
    private const string clickTag = "ClickableTag";
    public Action<Transform> betSelected;
    public bool isBettingActive = false;

    private void Update()
    {
        DetectClickable();
    }

    //Remember to set isBettingActive true for betting to work.
    private void DetectClickable()
    {
        //if (Input.touchCount != 1) return;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click check");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.transform.tag == clickTag && isBettingActive)
                {
                    Debug.Log(hit.transform.name);
                    betSelected?.Invoke(hit.transform);
                }
            }
        }
    }
}
