using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class StartScene : MonoBehaviour
{
    public  bool isExistSaveData = false;
    public Button LoadBtn;

    public void Start()
    {
        string IsFile = Path.Combine(Application.dataPath, "Json", "PlayerData.json"); //파일 경로, 이름
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
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("TutorialScene1");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Village_FINAL");
        isExistSaveData = true; 
    }

    public void EixtGame()
    {
        Application.Quit();
    }
}
