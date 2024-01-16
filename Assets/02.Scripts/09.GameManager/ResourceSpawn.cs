using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] resourcesPrefabs;

    [SerializeField] private Transform[] resourcesSpawnPoints;
    //[SerializeField] private GameObject[] resourcesSpawnPoints;

    [SerializeField] private GameObject[] resourcesCount;

    [SerializeField] private int currentResourcesCount = 0;
    [SerializeField] private int maxResourcesCount = 20;

    [SerializeField] private int spawnRadius = 5;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float maxSpawnDelay = 10f;

    private void Awake()
    {
        resourcesSpawnPoints = GetComponentsInChildren<Transform>();
        //for (int i = 0; i < resourcesCount.Length; i++)
        //{
        //    resourcesSpawnPoints = transform.GetChild(i).gameObject;
        //}
    }

    private void Start()
    {
        Debug.Log($"chiled °¹¼ö : {resourcesSpawnPoints.Length}");
    }

    void Update()
    {
        resourcesCount = GameObject.FindGameObjectsWithTag("Resource");
        currentResourcesCount = resourcesCount.Length;

        if (currentResourcesCount < maxResourcesCount)
        {
            spawnDelay += Time.deltaTime;
            if (spawnDelay >= maxSpawnDelay)
            {
                ResourceSpawnPoint();
                currentResourcesCount++;
                spawnDelay = 0;
            }
        }
    }

    //void Spawn()
    //{
    //    float randomX = transform.position.x + Random.Range(-spawnRadius, spawnRadius);
    //    float randomZ = transform.position.z + Random.Range(-spawnRadius, spawnRadius);
    //    Vector3 spawnPoint = new Vector3(randomX, 5, randomZ);
    //    int randomResources = Random.Range(0, ResourcesPrefabs.Length);

    //    Instantiate(ResourcesPrefabs[randomResources], spawnPoint, Quaternion.identity);
    //    //Instantiate(ResourcesPrefabs[randomResources], spawnPoint.position, spawnPoint.rotation);
    //}

    void ResourceSpawnPoint()
    {
        spawnRadius = 5;
        int randomX = Random.Range(-spawnRadius, spawnRadius);
        int randomZ = Random.Range(-spawnRadius, spawnRadius);

        //int randomX = transform.position.x + Random.Range(-spawnRadius, spawnRadius);
        //int randomZ = transform.position.z + Random.Range(-spawnRadius, spawnRadius);
        Vector3 spawnPoint = new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ);

        //int randomSpawnPoint = Random.Range(0, resourcesSpawnPoints.Length);
        //int randomResources = Random.Range(0, resourcesPrefabs.Length);

        //Instantiate(resourcesPrefabs[randomResources], resourcesSpawnPoints[randomSpawnPoint].position + spawnPoint, Quaternion.identity);
        GameObject Resource = GameManager.Instance.objectPoolManager.Get(Random.Range(0, GameManager.Instance.objectPoolManager.prefabs.Length));
        Resource.transform.position = resourcesSpawnPoints[Random.Range(1, resourcesSpawnPoints.Length)].position + spawnPoint;
    }
}
