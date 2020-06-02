using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType {random, room, hunt}

public class Unit : Entity
{

    public GameObject intro;
    public Transform player;
    Vector3 targetPos;
    public float speed = 1;
    float speedMultiplyer = .05f;
    public GridNode g;
    public Grid G;
    Vector3[] path;
    int targetIndex;
    public bool randomMovement;
    public bool rPosReached;
    public GameObject plane;
    public Animator anim;
    public float DistanceAllowed = 20;
    public float distance;
    public MovementType Move;
    public List<GameObject> Room = new List<GameObject>();
    public GameObject currentRoom;
    public float Timer = 15f;
    public int CurrentRoomIndex;

    //void Start()
    //{

    //    targetPos.position = new Vector3(range(0, plane.transform.position.x), range(0, plane.transform.position.y), 0);
    //    generateRandPos(targetPos.position);
    //}

    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        rPosReached = true;
        anim = GetComponent<Animator>();
        currentRoom = Room[0];
        Move = MovementType.room;
    }


    void Update()
    {

        distance = Vector3.Distance(player.position, transform.position);

        if (!intro.activeSelf)
        {
            if (transform.position != Vector3.zero) //animation code
            {
                Vector3 dir = targetPos - transform.position;
                dir.Normalize();
                anim.SetFloat("xInput", dir.x);
                anim.SetFloat("yInput", dir.y);
            }

            if (Move == MovementType.hunt)
            {
                Debug.Log("Movement Type hunt");
                PathRequestManager.RequestPath(transform.position, player.position, OnPathFound);
            }
            if (distance <= DistanceAllowed) //Hunting
            {
                
                Move = MovementType.hunt;

            }

            if( Move == MovementType.random)
            {
                Debug.Log("Movement Type Random");
                Timer-= .01f;
                if (rPosReached)
                {
                    Debug.Log("Randomizing...");
                    Random.InitState((int)System.DateTime.Now.Ticks);
                    targetPos = new Vector3(range(-9f, 10f), range(2f, 10f), 0);
                    g = G.NodeFromWorldPoint(targetPos);
                    if (g.walkable)
                    {
                        Debug.Log("heading...");
                        rPosReached = false;
                    }
                }

                //Follow Path
                if (distance > 1 && rPosReached == false)
                {
                    PathRequestManager.RequestPath(transform.position, targetPos, OnPathFound);

                }


                //check if location has been reached, if it has randomize again
                if (Vector3.Distance(targetPos, gameObject.transform.position) < 3)
                {
                    Debug.Log("Position Reached");
                    rPosReached = true;
                }

                if (Timer <= 0)
                {
                    Move = MovementType.room;
                    Timer = 1f;
                }
            }

            if(Move == MovementType.room)
            {
                Debug.Log("Movement Type Room");
                PathRequestManager.RequestPath(transform.position, currentRoom.transform.position, OnPathFound);
                if (transform.position == currentRoom.transform.position)
                {
                    Debug.Log("Room Reached");
                    GameObject T = Room[CurrentRoomIndex];
                    Room.RemoveAt(CurrentRoomIndex);
                    Room.Add(T);
                    CurrentRoomIndex++;
                    currentRoom = Room[CurrentRoomIndex];
                    Move = MovementType.random;
                }
            }

            if(distance> DistanceAllowed && Move == MovementType.hunt)
            {
                Move = MovementType.room;
            }

        }

    }

    public void generateRandPos(Vector3 target)
    {

        PathRequestManager.RequestPath(transform.position, target, OnPathFound);

    }



    public float range(float min, float max)
    {
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
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], new Vector3(.25f,.25f,.25f));
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
