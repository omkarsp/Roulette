using System;
using System.Collections.Generic;
using UnityEngine;

public class BallTarget: MonoBehaviour
{
    [SerializeField] private List<TargetMap> targetMaps;
    public int resultNum;
    public List<GameObject> targetObjects;

    public Transform GenerateRandomResult()
    {
        //call result method
        resultNum = UnityEngine.Random.Range(0, targetObjects.Count);
        Debug.Log(resultNum);
        return targetObjects[resultNum].transform;
    }

    public Transform GetResultTransform(int _resultNum)
    {
        Debug.Log(_resultNum);
        return targetObjects[_resultNum].transform;
    }

    public Collider GetCollider()
    {
        return targetObjects[resultNum].gameObject.GetComponent<BoxCollider>();
    }
}

[Serializable]
public struct TargetMap
{
    public int number;
    public Transform targetTransform;
}