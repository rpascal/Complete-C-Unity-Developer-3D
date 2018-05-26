using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour {


    [SerializeField] [Range(1f, 20f)] float gridSize = 10f;

    TextMesh textMesh;
    Vector3 snapPos;

    private void Start() {
        textMesh = GetComponentInChildren<TextMesh>();
        snapPos = transform.position;
    }

    void Update() {
        HandleMovementSnap();
    }

    private void HandleMovementSnap() {
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = new Vector3(snapPos.x, 0, snapPos.z);
        UpdateText();
    }

    private void UpdateText() {
        textMesh.text = $"{snapPos.x/gridSize},{snapPos.z / gridSize}";
    }

}
