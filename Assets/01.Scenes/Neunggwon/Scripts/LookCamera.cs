using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    void Start()
    {
        camera = Camera.main.gameObject;
    }

    private void LateUpdate()
    {
        if (camera != null)
        {
            transform.LookAt(camera.transform);
            transform.Rotate(0, 180, 0);
        }
    }
    
}
