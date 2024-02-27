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
        return string.Format("Assets/Json/{0}.json", fileName);
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
        File.WriteAllText(path, jsonData);
    }

    public T LoadJsonData<T>(string fileName)
    {
        string fullPath = GetJsonSavePath(fileName);
        if (!File.Exists(fullPath))
            return default(T);
        string jsonData = File.ReadAllText(fullPath);
        T loadData = JsonConvert.DeserializeObject<T>(jsonData);

        return loadData;
    }


}
