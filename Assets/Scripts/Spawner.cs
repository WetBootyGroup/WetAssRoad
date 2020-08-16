using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnables;

    float timer;
    public float spawnDelay;

    void Start()
    {
        timer = 2;
    }

    void Update()
    {
        if (timer <= 0) {
            SpawnObstacle();
            timer = spawnDelay;
        }
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
    }

    void SpawnObstacle() {
        float randX = Random.Range(-15 ,15);
        Vector3 spawnLocation = new Vector3(randX, 0, 0);
        GameObject toSpawn = spawnables[(Random.Range(0,3)) % 3];
        Instantiate(toSpawn, spawnLocation, Quaternion.identity);
    }
}
