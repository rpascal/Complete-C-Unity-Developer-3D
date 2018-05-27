﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public bool isExplored = false;
    public Waypoint exploredFrom;

    const int gridSize = 10;
    Vector2Int gridPos;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public Vector2Int GetGridPos() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public int GetGridSize() {
        return gridSize;
    }

    public void SetTopColor(Color color) {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    

}