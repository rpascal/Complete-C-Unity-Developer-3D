using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [Tooltip("In Seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathEffect;

    private void OnTriggerEnter(Collider other) {
        StartDeathSequence();
        deathEffect.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }


    private void StartDeathSequence() {
        SendMessage("OnPlayerDeath");

    }

    private void ReloadScene() {
        SceneManager.LoadScene(1);
    }


}
