using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToturoialDataLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("tutorial data load");
        ResourceManager.Instance.LoadData();
    }


}
