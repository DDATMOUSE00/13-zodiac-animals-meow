using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public class DataManager : MonoBehaviour
{

    public static DataManager I;
    private void Awake()
    {
        I = this;
    }

    private string GetJsonSavePath(string fileName)
    {
        return string.Format("Assets/Resources/Json/{0}.json", fileName);
    }

    public void SaveJsonData<T>(T data, string fileName)
    {
        Debug.Log("save json data");
        string jsonData = JsonConvert.SerializeObject(data,Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );

        string path = GetJsonSavePath(fileName);
        var file = new FileInfo(path); 
        Debug.Log($"save = {file.Name} ");
        File.WriteAllText(path, jsonData);
    }

    public T LoadJsonData<T>(string fileName)
    {
        string fullPath = GetJsonSavePath(fileName);
        if (!File.Exists(fullPath))
        {
            Debug.Log("doees not exist");
            return default(T);
        }
        Debug.Log("file exists");
        //string jsonData = File.ReadAllText(fullPath);
        var jsonData = Resources.Load($"Json/{fileName}");
        Debug.Log(jsonData.ToString());
        T loadData = JsonConvert.DeserializeObject<T>(jsonData.ToString());

        return loadData;
    }


}
