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
        AudioManager.instance.PlayBgm(true);
    }

    private void Info()
    {
        //objectPoolManager = GetComponentInChildren<ObjectPoolManager>();
        fadeManager = GetComponentInChildren<FadeManager>();
        player = GameObject.FindWithTag("Player");
    }

    public void QuitGame()
    {
        //ResourceManager.Instance.SaveData();
        AudioManager.instance.PlayBgm(false);
        Application.Quit();
    }
}
