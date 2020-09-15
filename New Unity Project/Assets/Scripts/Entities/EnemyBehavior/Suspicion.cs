using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Suspicion : MonoBehaviour
{
    public Player player;
    public Transform playerTransform;
    
    public CircleCollider2D circleCollider2D;
    public float maxClockTimer; // Start value of the clock timer.
    private float clockTimer;
    private float currentSuspicion = 0.0f;
    private float suspicionValueChange = 0.0f;
    public float maxSuspicion;
    private float distanceToPlayer;

    public float CurrentSuspicion
    {
        get => currentSuspicion;
        set => currentSuspicion = value;
    }
    
    private void ChangeSuspicionBasedOnBlockingObjects()
    {
        

        RaycastHit2D[] line = Physics2D.LinecastAll(gameObject.transform.position, playerTransform.transform.position);
        List<RaycastHit2D> listLine = line.ToList();
       listLine.RemoveAt(0);
       listLine.RemoveAt(listLine.Count - 1);
       var numBlockingObjects = listLine.Count;
       suspicionValueChange -= numBlockingObjects * 0.1f;
    }

    private float ChangeSuspicionIfEnemyIsNotInTheSameRoom()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        return renderer.isVisible ? 1.0f : 0.0f;
    }
    private void ChangeSuspicionBasedOnDistance()
    {
        distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if(currentSuspicion < maxSuspicion)
            suspicionValueChange += (1.0f / distanceToPlayer * 10.0f) - 1.0f;
        if (suspicionValueChange < 0.0f)
            suspicionValueChange = 0.0f;
        
    }

    private void ChangeSuspicionBasedOnCrowd()
    {
        suspicionValueChange -= 0.1f * player.NpcInRange;
    }

    private void ChangeSuspicionValue()
    {
        clockTimer -= Time.deltaTime;
        if (clockTimer < 0.0f)
        {
            
            // ADD ANY FUNCTIONS THAT MODIFIES CURRENT SUSPICION HERE!!
            
            ChangeSuspicionBasedOnDistance();
            ChangeSuspicionBasedOnBlockingObjects();
            ChangeSuspicionBasedOnCrowd();
            currentSuspicion += suspicionValueChange * ChangeSuspicionIfEnemyIsNotInTheSameRoom();
            Debug.Log(currentSuspicion);
            clockTimer = maxClockTimer;
            suspicionValueChange = 0.0f;
        }
    }
    private void Update()
    {
        ChangeSuspicionValue();

    }
    private void Start()
    {
        clockTimer = maxClockTimer;
        
        
    }
}
