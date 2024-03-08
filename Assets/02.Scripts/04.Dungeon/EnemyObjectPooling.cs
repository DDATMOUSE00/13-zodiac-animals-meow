using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPooling : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private int initialPoolSize = 4;
    private int maxPoolSize = 10;

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
                //CreateNewObject(obj.name, obj);
                pools[j].Add(obj);
            }
        }
    }

    GameObject CreateNewObject(string objName, GameObject Perfab)
    {
        var obj = Instantiate(Perfab, transform);
        obj.name = objName + "Pool";
        obj.SetActive(false);
        return obj;
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
