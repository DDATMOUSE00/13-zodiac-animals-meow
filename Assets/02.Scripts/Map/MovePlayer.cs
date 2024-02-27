using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        { 
            player = other.gameObject;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {
            StartCoroutine(TeleportRoutin());
        }
    }

    IEnumerator TeleportRoutin()
    {
        player.transform.position = destination.transform.position;
        yield return null;
    }
}
