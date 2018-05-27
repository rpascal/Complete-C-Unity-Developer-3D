using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    [SerializeField] readonly Tower towerPrefab;


    public bool isExplored = false;
    public Waypoint exploredFrom;
    public bool isPlaceable = true;

    const int gridSize = 10;
    Vector2Int gridPos;




    public Vector2Int GetGridPos() {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public int GetGridSize() {
        return gridSize;
    }


    private void OnMouseOver() {

        if (Input.GetMouseButton(0) && isPlaceable) {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
        }

    }



}
