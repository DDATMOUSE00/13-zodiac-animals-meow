using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunguonNPC : MonoBehaviour
{
    public GameObject dungeonDoor;
    public GameObject dungeonchoiceUI;
    //public GameObject player;
    //public BoxCollider boxCollider;

    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        dungeonDoor.SetActive(false);
    }

    public void Open()
    {
        dungeonchoiceUI.SetActive(true);
    }

    public void Close()
    {
        dungeonchoiceUI.SetActive(false);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    //other = capsuleCollider;
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("�÷��̾� ��");
    //        //dungeonDoor.SetActive(true);
    //        dungeonchoiceUI.SetActive(true);
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    //other = capsuleCollider;
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("�÷��̾� ����");
    //        //dungeonDoor.SetActive(false);
    //        dungeonchoiceUI.SetActive(false);
    //    }
    //}

}
