using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageScene : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject loadData;
    void Start()
    {
        loadData = GameObject.Find("StartScene");
        if (loadData.GetComponent<StartScene>().isExistSaveData)
        {
            ResourceManager.Instance.LoadData();
        }
    }
}
