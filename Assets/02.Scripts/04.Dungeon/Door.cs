using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject targetPoint; //�̵� �� ��ġ

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = targetPoint.transform.position;
    }
}
