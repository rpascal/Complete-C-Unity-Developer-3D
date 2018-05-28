using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;


    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other) {
        ProcessHit();
        if (hitPoints < 1) {
            KillEnemy();
        }
    }


    private void ProcessHit() {
        --hitPoints;
        hitParticlePrefab.Play();
        audioSource.PlayOneShot(enemyHitSFX);
    }

    private void KillEnemy() {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);

        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
