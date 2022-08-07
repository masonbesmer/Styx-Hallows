using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairySpawner : MonoBehaviour
{
    [SerializeField] GameObject FairyPrefab;
    [SerializeField] float spawnRate = 30f; //in seconds
    [SerializeField] Transform spawnPoint;

    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            SpawnFairy();
            ResetTimer();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void ResetTimer()
    {
        timer = spawnRate;
    }

    void SpawnFairy()
    {
        Instantiate(FairyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
