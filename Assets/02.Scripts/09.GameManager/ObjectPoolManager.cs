using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialPoolSize = 30; // 100
    [SerializeField] private int maxPoolSize = 100; // 10000

    private List<GameObject> objectPool;

    private void Start()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        if (objectPool.Count < maxPoolSize)
        {
            GameObject obj = Instantiate(prefab, transform);
            objectPool.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // 풀에 더 이상 생성할 수 없음
            return null;

        }
    }
}
