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
        dungeonDoor.SetActive(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //other = capsuleCollider;
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("플레이어 옴");
    //        //dungeonDoor.SetActive(true);
    //        dungeonchoiceUI.SetActive(true);
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    //other = capsuleCollider;
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("플레이어 나감");
    //        //dungeonDoor.SetActive(false);
    //        dungeonchoiceUI.SetActive(false);
    //    }
    //}
    
}
