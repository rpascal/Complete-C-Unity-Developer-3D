﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> towerQueue = new Queue<Tower>();


    public void AddTower(Waypoint baseWaypoint) {
        if (towerQueue.Count < towerLimit) {
            InstantiateNewTower(baseWaypoint);
        } else {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint) {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;
        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint) {
        var oldestTower = towerQueue.Dequeue();
        oldestTower.baseWaypoint.isPlaceable = true;

        newBaseWaypoint.isPlaceable = false;
        oldestTower.baseWaypoint = newBaseWaypoint;

        oldestTower.transform.position = newBaseWaypoint.transform.position;

        towerQueue.Enqueue(oldestTower);



    }
}
