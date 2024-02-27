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
        Info();
    }

    public void Start()
    {
        //player = GameObject.FindWithTag("Player");
    }

    private void Info()
    {
        objectPoolManager = GetComponentInChildren<ObjectPoolManager>();
        fadeManager = GetComponentInChildren<FadeManager>();
        player = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Debug.Log("Pooling");
        //    objectPoolManager.Get(1);
        //}
    }
}
