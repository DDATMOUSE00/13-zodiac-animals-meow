using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public DataManager I;
    private void Awake()
    {
        I = this;
    }

    public void SaveData<T>(T obj, string fileName, string path = Literals.JSON_PATH)
    {
        string jsonSting = JsonConvert.SerializeObject(obj, Formatting.Indented);
        File.WriteAllText($"{path}{fileName}.json", jsonSting);
    }


    public T LoadData<T>(string fileName, string path = Literals.JSON_PATH)
    {
        string fullPath = $"{path}{fileName}.json";

        if (!FindFile(fullPath))
            return default(T);

        string jsonData = File.ReadAllText($"{path}{fileName}.json");
        T loadData = JsonConvert.DeserializeObject<T>(jsonData);
        return loadData;
    }


    private bool FindFile(string path)
    {
        if (File.Exists(path))
            return true;

        Debug.Log($"File not found, Path : {path}");
        return false;
    }
}