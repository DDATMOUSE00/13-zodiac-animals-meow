using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager I;
    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ResourceManager Instance
    {
        get
        {
            if (I == null)
            {
                return new ResourceManager();
            }
            return I;
        }
    }

    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public void Test()
    {
        Debug.Log("test");
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        Debug.Log(path);
        GameObject prefab = Load<GameObject>($"Prefabs/{path}"); //Resources 에 있는 Prefab 폴더의 prefab 로드 

        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject obj)
    {
        if (obj == null) return;
        Object.Destroy(obj);
    }

}