using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    
    public Vector2Int coordinates; 
    public bool visited = false;
    public GameObject[] doors = new GameObject[4]; 

    void Start()
    {
        //foreach (GameObject door in doors)
        //{
        //    door.SetActive(false);
        //}
    }

    // 이 방에 들어올 때 호출됩니다.
    public void Enter()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }

    // 이 방에서 나갈 때 호출됩니다.
    public void Exit()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }
}
