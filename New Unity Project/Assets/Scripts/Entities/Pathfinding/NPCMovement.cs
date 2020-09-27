using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum NPCMovementType { random, room, close, feignHunt, Return }

public class NPCMovement : MonoBehaviour
{
    public GameObject DialogueBox;
    public float Speed = 2;
    public float XDirection, YDirection;
    private int behave;
    public Vector3 anchorPosition;
    Vector3 targetPos;
    public float distance, tetherdist;
    public Transform player;
    public Animator anim;
    public NPCMovementType Move;
    Vector3[] path;
    int targetIndex;
    public float NPCspeed = 1;
    public float xMin, xMax, yMin, yMax;
    private bool rPosReached;
    private Room nativeRoom;
    public float randX, randY;
    public float Timer = 15f;

    // Start is called before the first frame update
    void Start()
    {
        DialogueBox = GameObject.Find("Dialogue Box");
        behave = Random.Range(0, 5);
        XDirection = range(-5, 5);
        YDirection = range(-5, 5);
        anim = GetComponent<Animator>();
        xMin = anchorPosition.x - 1;
        xMax = anchorPosition.x + 1;
        yMin = anchorPosition.y - 1;
        yMax = anchorPosition.y + 1;

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        tetherdist = Vector3.Distance(new Vector3(anchorPosition.x, anchorPosition.y, anchorPosition.z), transform.position);
        if (!DialogueBox.activeSelf)
        {
            // transform.Translate(XDirection * Time.deltaTime, YDirection * Time.deltaTime, 0);
            if (Move == NPCMovementType.feignHunt)
            {
                //  Debug.Log("Movement Type hunt");
                PathRequestManager.RequestPath(transform.position, player.position, OnPathFound);
                if (tetherdist > 2)
                {
                    Move = NPCMovementType.Return;
                }

            }
            if (Move == NPCMovementType.random)
            {
                //    Timer -= .01f;
                //    if (rPosReached)
                //    {
                //        Random.InitState((int)System.DateTime.Now.Ticks);
                //        randX = range(xMin, xMax);
                //        randY = range(yMin, yMax);
                //        targetPos = new Vector3(randX, randY, 0);
                //        g = G.NodeFromWorldPoint(targetPos);
                //        if (g.walkable)
                //        {
                //            Debug.Log("heading...");
                //            // follow path
                //            PathRequestManager.RequestPath(transform.position, targetPos, OnPathFound);
                //            rPosReached = false;
                //        }
                //    }

                //    //Follow Path
                //    if (rPosReached == false)
                //    {
                //        PathRequestManager.RequestPath(transform.position, targetPos, OnPathFound); //  location change to above if statement's nested if

                //    }


                //    //check if location has been reached, if it has randomize again
                //    if (Vector3.Distance(targetPos, gameObject.transform.position) < 1)
                //    {
                //        Debug.Log("Position Reached");
                //        rPosReached = true;
                //    }

                //    if (Timer <= 0)
                //    {
                //        Move = NPCMovementType.room;
                //        Timer = 15f;
                //        rPosReached = true;
                //    }
            }
            if (Move == NPCMovementType.close)
            {

            }
            if (Move == NPCMovementType.Return) //Returns the NPC to its anchor position & select new behavior
            {
                PathRequestManager.RequestPath(transform.position, new Vector3(anchorPosition.x, anchorPosition.y, anchorPosition.z), OnPathFound);
                if (tetherdist < .75f)
                {
                    //select new NPC behavior
                    Move = NPCMovementType.random;
                }
            }
            if (Move == NPCMovementType.room)
            {

            }







        }
        //if (XDirection != 0f && YDirection != 0f)
        //{
        //    anim.SetFloat("xInput", XDirection);
        //    anim.SetFloat("yInput", YDirection);
        //}
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Speed *= -1;
        XDirection = range(-5, 5);
        YDirection = range(-5, 5);
    }
    public float range(float min, float max)
    {

        Random.InitState((int)System.DateTime.Now.Ticks * Random.Range(0, 10));
        return Random.Range(min, max + 1);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, NPCspeed * Time.deltaTime);
            yield return null;
        }
    }
}
