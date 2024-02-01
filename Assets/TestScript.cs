using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        List<int> keyList = new List<int>(ItemManager.I.itemDic.Keys);
        bool res = ItemManager.I.AddItem(itemsToPickup[id]);
        //Debug.Log(ItemManager.I.itemDic[id]);
        //Debug.Log(keyList);
        for(int i =0; i < keyList.Count; i++)
        {
            Debug.Log($"{keyList[i]} - {ItemManager.I.itemDic[keyList[i]]}");

        }
    }
}
