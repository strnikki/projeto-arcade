using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] EnemySpawn[] spawnPoints;

    private float timeElapsed;

    private void Start()
    {
        Invoke("SpawnEnemy", 3f);
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length - 1);

        Instantiate(enemy, spawnPoints[index].transform.position, enemy.transform.rotation);

        Invoke("SpawnEnemy", 1f);
    }
}
