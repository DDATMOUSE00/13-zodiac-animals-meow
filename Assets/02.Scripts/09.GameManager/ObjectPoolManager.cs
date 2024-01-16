using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private int initialPoolSize = 10;
    private int maxPoolSize = 30;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            for (int j = 0; j < pools.Length; j++)
            {
                GameObject obj = Instantiate(prefabs[j], transform);
                obj.SetActive(false);
                pools[j].Add(obj);
            }
        }
    }


    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }



        return select;
    }
}

