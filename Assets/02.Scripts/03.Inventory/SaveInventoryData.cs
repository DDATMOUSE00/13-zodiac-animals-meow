using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInventoryData : MonoBehaviour
{
    public GameObject inventoryContainer;
    public void SaveData()
    {
        inventoryContainer.SetActive(false);
        DataManager.I.SaveData(ItemManager.Instance.Items, "testItem");
    }

    public void LoadData()
    {
        inventoryContainer.SetActive(true);
    }
}
