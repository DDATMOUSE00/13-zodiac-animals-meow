using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2Int direction;
    public GameObject targetPoint; //이동 할 위치

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = targetPoint.transform.position;
            DungeonManager.Instance.playerLocation += direction;
            Debug.Log(DungeonManager.Instance.playerLocation);
        }
    }

    public void SetDirection(Vector2Int dir)
    {
        direction = dir;
    }
}
