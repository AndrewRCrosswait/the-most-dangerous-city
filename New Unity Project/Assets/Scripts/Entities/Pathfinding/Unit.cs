using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Entity
{

    public GameObject intro;
    public Transform target;
    public Transform targetPos;
    float speed = 10;
    float speedMultiplyer = .05f;
    public GridNode g;
    public Grid G;
    Vector3[] path;
    int targetIndex;
    public bool randomMovement;
    public bool rPosReached;
    public GameObject plane;


    //void Start()
    //{

    //    targetPos.position = new Vector3(range(0, plane.transform.position.x), range(0, plane.transform.position.y), 0);
    //    generateRandPos(targetPos.position);
    //}

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (!intro.activeSelf)
        {

            if (distance <= 20)
            {
                speed = 10;
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

            }


            else if (distance > 20)
            {
                speed = 5;

                //Randomize location to move to
                if (rPosReached)
                {
                    Random.InitState((int)System.DateTime.Now.Ticks);
                    targetPos.position = new Vector3(range(-16f, 16f), range(-16f, 16f), 0);
                    g = G.NodeFromWorldPoint(targetPos.position);
                    if (g.walkable)
                    {
                        rPosReached = false;
                    }
                }

                //Follow Path
                PathRequestManager.RequestPath(transform.position, targetPos.position, OnPathFound);

                //check if location has been reached, if it has randomize again
                if (Vector3.Distance(targetPos.position, gameObject.transform.position) < 3)
                {
                    Debug.Log("Position Reached");

                    rPosReached = true;
                }


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
                Gizmos.DrawCube(path[i], Vector3.one);
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
