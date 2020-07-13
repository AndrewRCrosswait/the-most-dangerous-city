using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void ChangeSuspicionBasedOnBlockingObjects()
    {
        /* Checks for objects that will lower suspicion by being in between player and enemy
         WORKS ONLY IF EVERY BLOCKING OBJECT HAS 2 COLLIDERS!!!
        Each NPC having two colliders makes it hard to determine number of blocking objects.
        */

        RaycastHit2D[] line = Physics2D.LinecastAll(gameObject.transform.position, player.transform.position);
        List<RaycastHit2D> listLine = line.ToList();
       listLine.RemoveAt(0);
       listLine.RemoveAt(listLine.Count - 1);
       var numBlockingObjects = listLine.Count;
       currentSuspicion -= numBlockingObjects * 0.1f;
    }
    private void ChangeSuspicionBasedOnDistance()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if(currentSuspicion < maxSuspicion)
            currentSuspicion += (1.0f / distanceToPlayer * 10.0f) - 1.0f;
        if (currentSuspicion < 0.0f)
            currentSuspicion = 0.0f;
        Debug.Log(currentSuspicion);
    }

 private void ChangeSuspicionValue()
    {
        clockTimer -= Time.deltaTime;
        if (clockTimer < 0.0f)
        {
            // ADD ANY FUNCTIONS THAT MODIFY CURRENT SUSPICION HERE!!
            ChangeSuspicionBasedOnDistance();
            ChangeSuspicionBasedOnBlockingObjects();
            clockTimer = maxClockTimer;
        }
    }
    private void Update()
    {
        ChangeSuspicionValue();

    }
    private void Start()
    {
        clockTimer = maxClockTimer;
        Debug.DrawLine(gameObject.transform.position, player.position, Color.white);
    }
}
