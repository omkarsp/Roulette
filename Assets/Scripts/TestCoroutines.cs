using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutines : MonoBehaviour
{
    int a = 5;

    private void Start()
    {
        Debug.Log(1);
        StartCoroutine(TestRoutine());
        Debug.Log(2);
        Debug.Log(Double(a));
        Debug.Log(a);
    }

    private int Double(int x)
    {
        x += 1;
        return x;
    }

    IEnumerator TestRoutine()
    {
        Debug.Log(3);
        //yield return null;
        Debug.Log(4);
        yield return new WaitForSeconds(1);
        Debug.Log(5);
    }
}
