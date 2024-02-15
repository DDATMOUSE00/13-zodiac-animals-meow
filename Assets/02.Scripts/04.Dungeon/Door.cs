using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject targetPoint; //이동 할 위치

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = targetPoint.transform.position;
    }
}
