using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Transform spawnPointsObject;
    [SerializeField] float spawnCooldown = 1f;
    [SerializeField] float startDelay = 3f;
    

    private float timeElapsed;
    private Transform[] spawnPoints;
    private bool spawnEnabled = false;

    
    private void Start()
    {
        
        spawnPoints = spawnPointsObject.GetComponentsInChildren<Transform>();
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawning " + this.gameObject.name);
        int index = Random.Range(0, spawnPoints.Length - 1);

        Instantiate(enemy, spawnPoints[index].transform.position, enemy.transform.rotation);

        if(spawnEnabled)
            Invoke("SpawnEnemy", spawnCooldown);
    }

    public void StartSpawning()
    {
        spawnEnabled = true;
        Invoke("SpawnEnemy", startDelay);
    }

    public void StopSpawning()
    {
        spawnEnabled = false;
    }
}
