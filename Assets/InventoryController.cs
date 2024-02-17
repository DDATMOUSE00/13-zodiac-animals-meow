using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject UI;

    public void SaveInventoryData()
    {
        UI.SetActive(false);
      //  Debug.Log(ItemManager.I.itemDic);
     //   DataManager.I.SaveJsonData(ItemManager.I.itemDic, "ItemData");

    }

    public void LoadInventoryData()
    {
        UI.SetActive(true);

        //ItemManager.I.itemDic = DataManager.I.LoadJsonData<Dictionary<int, int>>("ItemData");
    }
}
