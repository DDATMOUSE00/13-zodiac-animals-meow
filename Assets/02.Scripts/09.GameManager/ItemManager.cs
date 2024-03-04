
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    public static ItemManager I;
    public List<Item> totalItems = new List<Item>();

    public DescOfItemConatiner descbox = new DescOfItemConatiner();

    public Dictionary<int, int> itemDic = new Dictionary<int, int>();
    public List<Item> itemList = new List<Item>();
    public Slot[] slots;
    public GameObject objContainer;
    public GameObject splitContainer;
    public GameObject dropBtn;

    public TMP_Text gold;

    int selectedSlot = -1;


    private void Start()
    {
        selectedSlot = 0;
        gold.text = (GameManager.Instance.player.GetComponent<PlayerGold>().Gold).ToString();
    }
    void Awake()
    {
        I = this;
    }
    private Item findItemWithIdInTotalItem(int id)
    {
        foreach (var i in totalItems)
        {
            if (i.id == id)
            {
                return i;
            }
        }
        return null;
    }
    private Item findItemWithId(int id)
    {
        foreach(var i in itemList)
        {
            if(i.id == id)
            {
                return i;
            }
        }
        return null;
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
             //   items.Add(itemInNewSlot);
            }
        }
    }

    public void SaveInventoryData()
    {

        Debug.Log(itemDic);

        DataManager.I.SaveJsonData(itemDic, "ItemData");

    }

    public void LoadInventoryData()
    {
        itemDic = DataManager.I.LoadJsonData<Dictionary<int, int>>("ItemData");
        gold.text = (GameManager.Instance.player.GetComponent<PlayerGold>().Gold).ToString();
        if (itemDic.Count != 0)
        {
            List<int> keyList = new List<int>(itemDic.Keys);
 

            for (int i = 0; i < keyList.Count; i++)
            {
                Item item = findItemWithIdInTotalItem(keyList[i]);
                if (item != null)
                {
                    Debug.Log(item.id);
                    for (int j = 0; j < slots.Length; j++)
                    {
                        DraggableItem itemInNewSlot = slots[j].GetComponentInChildren<DraggableItem>();
                        if (itemInNewSlot == null)
                        {
                            SpawnLoadItem(item, slots[j]);
                            break;

                        }
                    }
                }

            }
        }
    }
    public void RefreshInventorySlot()
    {
        foreach (var s in slots)
        {
            DraggableItem dragItem = s.GetComponentInChildren<DraggableItem>();
            if (dragItem != null)
            {
                if (itemDic[dragItem.item.id] > 0)
                {
                    dragItem.bundle = itemDic[dragItem.item.id];
                    dragItem.RefreshCount();
                }
                else if (itemDic[dragItem.item.id] <= 0)
                {
                    Destroy(dragItem.gameObject);
                }
            }
        }
        gold.text = (GameManager.Instance.player.GetComponent<PlayerGold>().Gold).ToString();
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



    public void ShowToolTip(DraggableItem draggableItem, Vector3 position)
    {
        Item item = findItemWithId(draggableItem.item.id);
        objContainer.SetActive(true);
      
       objContainer.transform.position = new Vector3(position.x , position.y-200 , position.z);

        descbox.Setting(item);
    }
    public void HideToolTip()
    {
        objContainer.SetActive(false);
    }

    private void SpawnNewItem(Item item, Slot slot)
    {
        GameObject itemPrefab = Resources.Load("DraggableItem") as GameObject;
        GameObject newItem = Instantiate(itemPrefab, slot.transform);

        DraggableItem inventoryItem = newItem.GetComponent<DraggableItem>();
       inventoryItem.InitializeItem(item, 1);

    }
    private void SpawnLoadItem(Item item, Slot slot)
    {
        itemList.Add(item);
        GameObject itemPrefab = Resources.Load("DraggableItem") as GameObject;
        GameObject newItem = Instantiate(itemPrefab, slot.transform);

        DraggableItem inventoryItem = newItem.GetComponent<DraggableItem>();

        inventoryItem.InitializeItem(item, itemDic[item.id]);

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

    public bool ChekInventoryItem(int id, int value)
    {
        if (itemDic.ContainsKey(id))
        {
            return itemDic[id] >= value;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(int id, int value)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Slot slot = slots[i];

            DraggableItem itemInNewSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInNewSlot != null && itemInNewSlot.item.id == id && itemInNewSlot.item.stackable)
            {
                if (itemInNewSlot.bundle >= value)
                {
                    Debug.Log($"B-item.{id}/ {itemInNewSlot.bundle}");
                    itemInNewSlot.bundle -= value;
                    itemInNewSlot.RefreshCount();
                    itemDic[id] = itemInNewSlot.bundle;
                    RefreshInventorySlot();
                    Debug.Log($"A-item.{id}/ {itemInNewSlot.bundle}");
                }
            }
        }
    }
}