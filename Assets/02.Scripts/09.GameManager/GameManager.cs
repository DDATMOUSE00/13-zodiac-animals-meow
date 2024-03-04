using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public ObjectPoolManager objectPoolManager;
    public FadeManager fadeManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Info();
    }

    public void Start()
    {
        //player = GameObject.FindWithTag("Player");
        //AudioManager.Instance.PlayBgm(true);
    }

    public void Info()
    {
        //objectPoolManager = GetComponentInChildren<ObjectPoolManager>();
        fadeManager = GetComponentInChildren<FadeManager>();
        player = GameObject.FindWithTag("Player");
    }

    public void QuitGame()
    {
        //ResourceManager.Instance.SaveData();
        Debug.Log("종료합니다,");
        AudioManager.Instance.PlayBgm(false);
        ResourceManager.Instance.SaveData();
        Debug.Log("정보 저장");
        Application.Quit();
    }
}
