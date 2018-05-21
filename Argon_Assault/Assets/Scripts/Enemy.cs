using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 10;


    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;


    ScoreBoard scoreBoard;

    // Use this for initialization
    void Start() {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider() {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }


    private void OnParticleCollision(GameObject other) {
        ProcessHit();
        if (hits <= 0) {
            KillEnemy();
        }
    }

    private void ProcessHit() {
        scoreBoard.ScoreHit(scorePerHit);
        --hits;
    }

    private void KillEnemy() {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
