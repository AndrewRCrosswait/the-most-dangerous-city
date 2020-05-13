﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool displayGridGizmos;
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    GridNode[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        createGrid();
        
    }


    void createGrid()
    {
        grid = new GridNode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x /2 - Vector3.up * gridWorldSize.y / 2;
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = (!Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new GridNode(walkable, worldPoint,x,y);
            }
        }
    }

    public List<GridNode> getNeighbours(GridNode Node)
    {
        List<GridNode> neighbours = new List<GridNode>();

        for(int x = -1; x <=1; x++){
            for(int y = -1; y<=1; y++)
            {
                if(x ==0 && y == 0)
                {
                    continue;
                }
                int checkX = Node.gridX + x;
                int checkY = Node.gridY + y;
                if(checkX >= 0 && checkX < gridSizeX && checkY >=0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }


    public GridNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
        
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        
        if (grid != null && displayGridGizmos)
        {
            foreach (GridNode n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white:Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

}