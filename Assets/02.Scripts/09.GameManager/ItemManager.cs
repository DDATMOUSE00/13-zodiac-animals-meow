using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public static ItemManager I;
    public List<DraggableItem> items = new List<DraggableItem>();

    public Dictionary<int, int> itemDic = new Dictionary<int, int>();
    public List<Item> itemList = new List<Item>();
    public Slot[] slots;
    public GameObject objContainer;
    public GameObject splitContainer;
    public GameObject dropBtn;

    int selectedSlot = -1;


    private void Start()
    {
        selectedSlot = 0;
    }
    void Awake()
    {
        I = this;
    }

    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
            slots[selectedSlot].DeSelect();

        slots[newValue].Select();
        selectedSlot = newValue;
    }
    public void UpdateItemsList()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];

            DraggableItem itemInNewSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInNewSlot != null)
            {
                items.Add(itemInNewSlot);
            }
        }
    }

    public void UpdateBundle(int id, int quantity, string type)
    {

        if (type == "sell")
        {
            itemDic[id] = itemDic[id] - quantity;

        }
        else if (type == "buy")
        {
            itemDic[id] = itemDic[id] + quantity;
        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];

            DraggableItem itemInNewSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInNewSlot != null && itemInNewSlot.item.id == item.id && itemInNewSlot.item.stackable)
            {
                itemInNewSlot.bundle++;
                itemInNewSlot.RefreshCount();
                itemDic[item.id] = itemInNewSlot.bundle;
                return true;
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];

            DraggableItem itemInNewSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInNewSlot == null)
            {

                SpawnNewItem(item, slot);

                if (!itemList.Contains(item))
                {
                    itemList.Add(item);
                    itemDic.Add(item.id, 1);

                }
                else
                {
                    int v = itemDic[item.id];
                    v++;
                    itemDic[item.id] = v;
                }


                return true;
            }

        }


        return false;

    }

    private void SpawnNewItem(Item item, Slot slot)
    {
        GameObject itemPrefab = Resources.Load("DraggableItem") as GameObject;
        GameObject newItem = Instantiate(itemPrefab, slot.transform);

        DraggableItem inventoryItem = newItem.GetComponent<DraggableItem>();
        inventoryItem.InitializeItem(item);
    }


    public Item GetSelectedItem()
    {
        Slot slot = slots[selectedSlot];
        DraggableItem itemInNewSlot = slot.GetComponentInChildren<DraggableItem>();
        if (itemInNewSlot != null)
        {
            return itemInNewSlot.item;
        }

        return null;
    }

    public void UseSelectedItem()
    {
        Slot slot = slots[selectedSlot];
        DraggableItem itemInNewSlot = slot.GetComponentInChildren<DraggableItem>();

        Item item = itemInNewSlot.item;
        itemInNewSlot.bundle--;
        if (itemInNewSlot.bundle <= 0)
        {
            Destroy(itemInNewSlot.gameObject);
            itemList.Remove(item);

        }
        else
        {
            itemInNewSlot.RefreshCount();

        }
    }
}