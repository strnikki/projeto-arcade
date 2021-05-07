using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnCooldown = 1f;
    [SerializeField] float startDelay = 3f;

    private float timeElapsed;

    
    private void Start()
    {
        Invoke("SpawnEnemy", startDelay);
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length - 1);

        Instantiate(enemy, spawnPoints[index].transform.position, enemy.transform.rotation);

        Invoke("SpawnEnemy", spawnCooldown);
    }
}
