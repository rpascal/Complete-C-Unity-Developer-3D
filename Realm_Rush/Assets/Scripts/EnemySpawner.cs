using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [Range(.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnedEnemies;
    int score = 0;


    // Use this for initialization
    void Start() {

        StartCoroutine(SpawnEnemies());
        spawnedEnemies.text = score.ToString();
    }

    IEnumerator SpawnEnemies() {
        while (true) {
            UpdateScore();
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void UpdateScore() {
        score++;
        spawnedEnemies.text = score.ToString();
    }
}
