using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testtt : MonoBehaviour
{
    [SerializeField] private float _speed = 30.0f;
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, _speed) *Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0.0f, 0.0f, _speed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(_speed, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(_speed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }
}
