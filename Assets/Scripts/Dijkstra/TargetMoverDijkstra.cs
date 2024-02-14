using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TargetMoverDijkstra : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTilesWeight allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

   // [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
   // [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    private int[] weights;
    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget)
    {
        if (targetInWorld != newTarget)
        {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget()
    {
        return targetInWorld;
    }

    private TilemapWeightGraph tilemapGraph = null;
    [SerializeField] private float timeBetweenSteps;

    protected virtual void Start()
    {

        tilemapGraph = new TilemapWeightGraph(tilemap, allowedTiles);
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = Dijkstra.GetPath(tilemapGraph, startNode, endNode);
        Debug.Log("shortestPath = " + string.Join(" , ", shortestPath));
        if (shortestPath.Count >= 2)
        {
            Vector3Int nextNode = shortestPath[1];
            transform.position = tilemap.GetCellCenterWorld(nextNode);
            timeBetweenSteps = GetNewTimeBetweenSteps(tilemap.GetTile(nextNode));
        }
        else
        {
            atTarget = true;
        }

    }


    private float GetNewTimeBetweenSteps(TileBase currentTile)
    {
        return 1 / (speed * 1 / allowedTiles.GetWeight(currentTile));
    }


}