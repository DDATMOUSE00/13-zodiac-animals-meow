using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    public static Test_Inventory Instence;
    public Slot[] slots = new Slot[20];

    private void Awake()
    {
        if (Instence == null)
        {
            Instence = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectItem(TestSlot slotData)
    {
        string SelectItemName = slotData.itemData.name;
        int selectItemCount = slotData.Count;

        Debug.Log(SelectItemName);
        Debug.Log(selectItemCount);
    }

}
