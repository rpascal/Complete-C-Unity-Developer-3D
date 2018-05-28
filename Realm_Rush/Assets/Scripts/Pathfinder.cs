using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint, endWaypoint;


    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;

    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.down
    };


    public List<Waypoint> GetPath() {
        if (path.Count == 0) {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath() {
        LoadBlocks();
        BreadthFirstSearch();
        GeneratePath(endWaypoint);
    }

    private void GeneratePath(Waypoint waypoint) {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
        if (waypoint == startWaypoint) {
            path.Reverse();
            return;
        }
        GeneratePath(waypoint.exploredFrom);
    }



    private void CreatePath() {
        SetAsPath(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint) {
            previous = previous.exploredFrom;
            SetAsPath(previous);
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }


    private void SetAsPath(Waypoint waypoint) {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }


    private void BreadthFirstSearch() {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning) {
            searchCenter = queue.Dequeue();
            HalfIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HalfIfEndFound() {
        if (searchCenter == endWaypoint) {
            isRunning = false;
        }
    }

    private void ExploreNeighbours() {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions) {
            Vector2Int explorationCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(explorationCoordinates)) {
                Waypoint neighbor = grid[explorationCoordinates];
                QueueNewNeighbor(neighbor);
            }
        }
    }

    private void QueueNewNeighbor(Waypoint neighbor) {
        if (!neighbor.isExplored && !queue.Contains(neighbor)) {
            queue.Enqueue(neighbor);
            neighbor.exploredFrom = searchCenter;
        }
    }


    private void LoadBlocks() {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints) {
            if (grid.ContainsKey(waypoint.GetGridPos())) {
                Debug.LogWarning($"Skipping overlapping block {waypoint}");
            } else {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }


}
