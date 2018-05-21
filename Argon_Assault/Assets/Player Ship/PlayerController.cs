using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 8f;
    [Tooltip("In m")] [SerializeField] float yTopRange = 4f;
    [Tooltip("In m")] [SerializeField] float yBottomRange = -4f;
    [SerializeField] GameObject[] guns;


    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -3.5f;
    [SerializeField] float positionYawFactor = 2.8f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;




    float xThrow;
    float yThrow;
    bool isControlEnabled = true;

    // Use this for initialization
    void Start() {

    }

    void OnPlayerDeath() { //called by string reference
        isControlEnabled = false;
    }


    // Update is called once per frame
    void Update() {
        if (isControlEnabled) {
            transform.localPosition = new Vector3(GetXPos(), GetYPos(), transform.localPosition.z);
            ProcessRotation();
            ProcessFiring();

        }
    }

    private void ProcessFiring() {
        if (CrossPlatformInputManager.GetButton("Fire")) {
            SetGunsActive(true);
        } else {
            SetGunsActive(false);
        }

    }

    private void SetGunsActive(bool isActive) {
        foreach (GameObject gun in guns) {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }


    private void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float positionDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + positionDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private float GetXPos() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        return clampedXPos;
    }

    private float GetYPos() {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawNewYPos, yBottomRange, yTopRange);
        return clampedYPos;
    }
}
