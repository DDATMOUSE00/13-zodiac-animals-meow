using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public GameObject settingUI;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("UI");
                go.AddComponent<UIManager>();
                _instance = go.GetComponent<UIManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
        set
        {
            if (_instance == null) _instance = value;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (_instance != this) Destroy(this);
            if (SceneManager.GetActiveScene().buildIndex == 0) Destroy(this);
        }

    }
    private void Start()
    {
        //settingUI.SetActive(false);
    }

    public void SettingButton()
    {
        if (settingUI.activeInHierarchy)
        {
            settingUI.SetActive(false);
        }
        else
        {
            settingUI.SetActive(true);
        }
    }
}
