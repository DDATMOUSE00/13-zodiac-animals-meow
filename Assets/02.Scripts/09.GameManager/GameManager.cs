using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

    
    private void Info()
    {
        objectPoolManager = GetComponentInChildren<ObjectPoolManager>();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pooling");
            objectPoolManager.Get(1);
        }
    }
}
