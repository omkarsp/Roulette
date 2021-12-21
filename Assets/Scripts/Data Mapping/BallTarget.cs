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
        resultNum = UnityEngine.Random.Range(0, targetObjects.Count);
        Debug.Log(resultNum);
        return targetObjects[resultNum].transform;
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