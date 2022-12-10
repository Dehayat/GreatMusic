using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnInterval = 10f;
    public float spawnProbability = 0.7f;
    public GameObject prefab;

    private float lastSpawn;


    private void Update()
    {
        if (lastSpawn + spawnInterval < Time.time)
        {
            lastSpawn = Time.time;
            if (Random.Range(0f, 1f) < spawnProbability)
            {
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
