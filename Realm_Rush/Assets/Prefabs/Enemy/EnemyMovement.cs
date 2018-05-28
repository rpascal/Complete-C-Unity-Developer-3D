using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float movementPeriod = .5f;
    [SerializeField] ParticleSystem goalParticlePrefab;


    // Use this for initialization
    void Start() {
        Pathfinder pathFinder = FindObjectOfType<Pathfinder>();
        var path = pathFinder.GetPath();

        StartCoroutine(FollowPath(path));

    }


    private IEnumerator FollowPath(List<Waypoint> path) {
        foreach (Waypoint waypoint in path) {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct();
    }


    private void SelfDestruct() {
        var vfx = Instantiate(goalParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);

        Destroy(gameObject);
    }
}
