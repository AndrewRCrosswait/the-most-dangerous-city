using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehavior : MonoBehaviour
{
    public float Smoothing;
    public Vector2 MaxPosition;
    public Vector2 MinPosition;
    
    private void LateUpdate()
    {
        Vector3 TargetPostion = new Vector3(transform.position.x,transform.position.y,transform.position.z); //get rid of
        TargetPostion.x = Mathf.Clamp(TargetPostion.x,MinPosition.x,MaxPosition.x); // make this gameobject.position = position.x,min,max
        TargetPostion.y = Mathf.Clamp(TargetPostion.y,MinPosition.y,MaxPosition.y); //same
        transform.position = Vector3.Lerp(transform.position,TargetPostion,Smoothing); //this should work
    }
}
