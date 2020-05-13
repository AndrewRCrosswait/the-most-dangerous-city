using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositon : MonoBehaviour
{

    public Vector3[] startPos;
    public Vector3[] endPos;

    public int range(int min, int max)
    {
        return Random.Range(min, max +1);
    }


    void Start()
    {
       
    }



}
