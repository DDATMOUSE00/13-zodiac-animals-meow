using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageFinal : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

        ResourceManager.Instance.LoadData();
    }


}
