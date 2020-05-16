using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehavior : MonoBehaviour
{
    public Transform Target;
    public float Smoothing;
    public Vector2 MaxPosition;
    public Vector2 MinPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 TargetPostion = new Vector3(Target.position.x,Target.position.y,transform.position.z); //get rid of
        TargetPostion.x = Mathf.Clamp(TargetPostion.x,MinPosition.x,MaxPosition.x); // make this gameobject.position = position.x,min,max
        TargetPostion.y = Mathf.Clamp(TargetPostion.y,MinPosition.y,MaxPosition.y); //same
        transform.position = Vector3.Lerp(transform.position,TargetPostion,Smoothing); //this should work
    }
}
