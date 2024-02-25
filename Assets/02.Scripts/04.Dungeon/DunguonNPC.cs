using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunguonNPC : MonoBehaviour
{
    public GameObject dungeonDoor;
    public GameObject dungeonchoiceUI;
    public GameObject player;
    public CapsuleCollider capsuleCollider;

    private void Awake()
    {
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        //other = capsuleCollider;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 옴");
            dungeonchoiceUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //other = capsuleCollider;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 나감");
            dungeonchoiceUI.SetActive(false);
        }
    }
    
}
