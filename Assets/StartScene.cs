using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class StartScene : MonoBehaviour
{

    public Button LoadBtn;

    public void Start()
    {
        string IsFile = Path.Combine(Application.dataPath, "Json", "PlayerData.json"); //파일 경로, 이름
        //    //Assets/Json
        Debug.Log(IsFile);
        Debug.Log(File.Exists(IsFile));
        if (File.Exists(IsFile)) 
        {
            LoadBtn.interactable = true;

        }
        else 
        {  
            LoadBtn.interactable = false;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Village_FINAL");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Village_FINAL");
        ResourceManager.Instance.LoadData();
    }
}
