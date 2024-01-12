using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float range = 2f;


    private void Update()
    {
        float distanceTarget = Vector3.Distance(transform.position, target.position);
        
        if (distanceTarget < range)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
