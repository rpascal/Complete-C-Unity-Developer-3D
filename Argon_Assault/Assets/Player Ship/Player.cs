using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {


    [Tooltip("In ms^-1")] [SerializeField] float speed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 8f;

    [Tooltip("In m")] [SerializeField] float yTopRange = 4f;
    [Tooltip("In m")] [SerializeField] float yBottomRange = -4f;

    [SerializeField] float positionPitchFactor = -3.5f;
    [SerializeField] float controlPitchFactor = -20f;

    [SerializeField] float positionYawFactor = 2.8f;
    [SerializeField] float controlRollFactor = -20f;


    float xThrow;
    float yThrow;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.localPosition = new Vector3(GetXPos(), GetYPos(), transform.localPosition.z);
        ProcessRotation();
    }

    private void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float positionDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + positionDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow* controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private float GetXPos() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * speed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        return clampedXPos;
    }

    private float GetYPos() {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * speed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawNewYPos, yBottomRange, yTopRange);
        return clampedYPos;
    }
}
