using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager:MonoBehaviour
{

    private static ResourceManager i;
    
    public void Awake()
    {
        if ( i == null)
        {
            i = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        } 
    }
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
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