using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] resourcesPrefabs;
    [SerializeField] private Transform[] resourcesSpawnPoints;

    private GameObject[] resourcesCount;
    //public Transform spawnPoint;
    [SerializeField] private int currentResourcesCount = 0;
    [SerializeField] private int maxResourcesCount = 3;

    [SerializeField] private float spawnRadius = 50f;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float maxSpawnDelay = 3f;


    void Update()
    {
        resourcesCount = GameObject.FindGameObjectsWithTag("tag");
        currentResourcesCount = resourcesCount.Length;

        if (currentResourcesCount < maxResourcesCount)
        {
            spawnDelay += Time.deltaTime;
            if (spawnDelay >= maxSpawnDelay)
            {
                ResourcespawnPoint();
                currentResourcesCount++;
                spawnDelay = 0;
            }
        }
    }

    //void NPCSpawn()
    //{
    //    float randomX = transform.position.x + Random.Range(-spawnRadius, spawnRadius);
    //    float randomZ = transform.position.z + Random.Range(-spawnRadius, spawnRadius);
    //    Vector3 spawnPoint = new Vector3(randomX, 5, randomZ);
    //    int randomResources = Random.Range(0, ResourcesPrefabs.Length);

    //    Instantiate(ResourcesPrefabs[randomResources], spawnPoint, Quaternion.identity);
    //    //Instantiate(ResourcesPrefabs[randomResources], spawnPoint.position, spawnPoint.rotation);
    //}

    void ResourcespawnPoint()
    {
        spawnRadius = 5;
        float randomX = transform.position.x + Random.Range(-spawnRadius, spawnRadius);
        float randomZ = transform.position.z + Random.Range(-spawnRadius, spawnRadius);
        Vector3 spawnPoint = new Vector3(randomX, 0, randomZ);

        int randomSpawnPoint = Random.Range(0, resourcesSpawnPoints.Length);

        int randomResources = Random.Range(0, resourcesPrefabs.Length);
        
        Instantiate(resourcesPrefabs[randomResources], resourcesSpawnPoints[randomSpawnPoint].position + spawnPoint, Quaternion.identity);
    }
}
