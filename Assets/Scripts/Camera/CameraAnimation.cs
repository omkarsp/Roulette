using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //Called when spin button is clicked
    public void MoveToWheel()
    {
        anim.Play("TableToWheel");
    }

    public void MoveToTable()
    {
        anim.Play("WheelToTable");
    }
}
