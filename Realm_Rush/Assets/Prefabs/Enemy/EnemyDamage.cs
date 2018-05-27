using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;


    // Use this for initialization
    void Start() {

    }


    private void OnParticleCollision(GameObject other) {
        ProcessHit();
        if (hitPoints < 1) {
            KillEnemy();
        }
    }


    private void ProcessHit() {
        --hitPoints;
    }

    private void KillEnemy() {
        Destroy(gameObject);
    }
}
