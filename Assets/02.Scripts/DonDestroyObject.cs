using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroyObject : MonoBehaviour
{
    public GameObject GameObject;
    private void Awake()
    {

        //if (GameObject != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
            
        //}
        DontDestroyOnLoad(this.gameObject);
    }
}
