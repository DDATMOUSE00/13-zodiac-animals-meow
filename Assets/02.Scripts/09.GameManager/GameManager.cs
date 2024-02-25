using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public ObjectPoolManager objectPoolManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
