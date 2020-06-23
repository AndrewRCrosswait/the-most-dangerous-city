using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Suspicion : MonoBehaviour
{
    public Transform player; 
    public float maxClockTimer; // Start value of the clock timer.
    private float clockTimer;
    public float currentSuspicion = 0.0f;
    public float maxSuspicion;
    private float distanceToPlayer;

    void ChangeSuspicionBasedOnDistance()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if(currentSuspicion < maxSuspicion)
        currentSuspicion += (1.0f / distanceToPlayer * 10.0f) - 1.0f;
        if (currentSuspicion < 0.0f)
            currentSuspicion = 0.0f;
        Debug.Log(currentSuspicion);
    }

 void ChangeSuspicionValue()
    {
        clockTimer -= Time.deltaTime;
        if (clockTimer < 0.0f)
        {
            // ADD ANY FUNCTIONS THAT MODIFY CURRENT SUSPICION HERE!!
            ChangeSuspicionBasedOnDistance();
            
            clockTimer = maxClockTimer;
        }
    }
    void Update()
    {
        ChangeSuspicionValue();

    }
    private void Start()
    {
        clockTimer = maxClockTimer;
    }
}
