using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Grid grid;
    public Transform seeker, target;
    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        GridNode startNode = grid.NodeFromWorldPoint(startPos);
        GridNode targetNode = grid.NodeFromWorldPoint(targetPos);

        List<GridNode> openSet = new List<GridNode>();
        HashSet<GridNode> closedSet = new HashSet<GridNode>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            GridNode currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fcost < currentNode.fcost || openSet[i].fcost == currentNode.fcost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (GridNode neighbour in grid.getNeighbours(currentNode))
            {
                if(!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
           

        }
    }

    void RetracePath(GridNode startNode, GridNode endNode)
    {
        List<GridNode> path = new List<GridNode>();
        GridNode CurrentNode = endNode;

        while(CurrentNode != startNode)
        {
            path.Add(CurrentNode);
            CurrentNode = CurrentNode.parent;
        }
        path.Reverse();
        grid.path = path;

    }


    int getDistance(GridNode nodeA, GridNode nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
