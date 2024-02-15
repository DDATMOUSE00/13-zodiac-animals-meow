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

    // �� �濡 ���� �� ȣ��˴ϴ�.
    public void Enter()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }

    // �� �濡�� ���� �� ȣ��˴ϴ�.
    public void Exit()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }
}
