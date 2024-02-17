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

    public void SaveData()
    {
        ItemManager.I.SaveInventoryData();
        QuestManager.I.SaveQuestData();
    }

    public void LoadData()
    {
        ItemManager.I.LoadInventoryData();
        QuestManager.I.LoadQuestData();
    }
    public static ResourceManager Instance
    {
        get
        {
            if (I == null)
            {
                ResourceManager resourceManager = new ResourceManager();
                return resourceManager;
            }
            return I;
        }
    }

    public T Load<T>(string path) where T : Object
    {
        Debug.Log(path);
        return Resources.Load<T>(path);
    }



    public GameObject Instantiate(string path, Transform parent = null)
    {

        GameObject prefab = Load<GameObject>($"Prefabs/{path}.prefab"); //Resources 에 있는 Prefab 폴더의 prefab 로드 

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