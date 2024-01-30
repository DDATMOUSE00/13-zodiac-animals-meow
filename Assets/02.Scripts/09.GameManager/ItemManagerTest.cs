using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerTest : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<Test_Slot> slots = new List<Test_Slot>();
  
    public static ItemManagerTest I;

    private void Awake()
    {
        I = this;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
    private int SelecteItemQuantity(Item _itemData)
    {
        int selecItem_Quantity = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (_itemData.id == slots[i].itemData.id)
            {
                selecItem_Quantity += slots[i].bundle;
            }
        }
        return selecItem_Quantity;
    }
    public bool IsCheckQuantityIsZero(Item item, int idx)
    {
        int totalQuantity = SelecteItemQuantity(item);
        if(totalQuantity - slots[idx].bundle <= 0)
        {
            return true;
        }
        return false;
    }

    public void StackItem(int idx1, int idx2)
    {
        Test_Slot item1 = slots[idx1];
        Test_Slot item2 = slots[idx2];
        item1.bundle = item1.bundle + item2.bundle;
    }

    public void SplitItem(int idx, int usrInput)
    {
        Test_Slot item = slots[idx];
        foreach(var slot in slots)
        {
            if (!slot.gameObject.activeSelf)
            {
                if(slots[idx].bundle - usrInput < 1)
                {
                    Debug.Log("½ÇÆÐ");
                    break;
                }

            }
        }
    }

}
