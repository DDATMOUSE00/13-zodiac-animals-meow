using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {

        bool res = ItemManager.I.AddItem(itemsToPickup[id]);
        if (res)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }
}
