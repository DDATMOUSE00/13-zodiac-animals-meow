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
                return true;
            }
        }
        for (int i =0; i < slots.Length; i++)
        {
            Slot slot = slots[i];

            DraggableItem itemInNewSlot = slot.GetComponentInChildren<DraggableItem>();
            if(itemInNewSlot == null )
            {
                SpawnNewItem(item, slot);
                itemList.Add(item);
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
    /*
    public bool RemoveItem(Item item) //Drop Item 
    {
        Item selectedItem = Items.Find(i => i.Name == item.Name);
        if (selectedItem != null)
        {
            bool IsCheckQuantityZero = (Int32.Parse(selectedItem.Quantity) - Int32.Parse(item.Bundle) == 0 ? true : false);
            if (IsCheckQuantityZero)
            {
                Debug.Log($"{item.Name} removed");
              Items.Remove(item);
            }
            else
            {
                item.Quantity = (Int32.Parse(selectedItem.Quantity) - Int32.Parse(item.Bundle)).ToString();
            }
            return true;
        }
        return false;
    }
    */
    /*
public bool IsCheckItemInList(Item item)
{
    Item selectedItem = Items.Find(i => i.Name == item.Name);
    if (selectedItem != null)
        return true;

    return false;
}

public void SplitItem(Transform t, Item item, string quantity)
{
 foreach(var slot in slots)
 {
    if(slot.childCount == 0)
     {
         기존 quantity update
int orginBundle = Int32.Parse(item.Bundle);
            int updateBundle = orginBundle - Int32.Parse(quantity);

            if(updateBundle < 1)
            {
                Debug.Log("실패");
                break;
            }
            item.Bundle = updateBundle.ToString();
            var originBundle = t.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
            originBundle.text = item.Bundle;


            새로운 슬롯아이템
            GameObject tmp = Resources.Load("Item") as GameObject;
            GameObject obj = Instantiate(tmp);
            obj.transform.SetParent(slot);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
            obj.transform.localScale = new Vector3(1, 1, 1);
            itemName.text = item.Name;
            itemIcon.sprite = item.Icon;
            itemQuantity.text = quantity;

            splitContainer.SetActive(false);
            //---------------------------------
            break;

        }  
    }    
}
public void StackItem(GameObject item1, GameObject item2)
{
    var item1Quantity = item1.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
    var item2Quantity = item2.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
    int totalQuantity = Int32.Parse(item1Quantity.text) + Int32.Parse(item2Quantity.text);
    var item1Name = item1.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
    Item selectedItem = Items.Find(x => x.Name == item1Name.text);
    item1Quantity.text = totalQuantity.ToString();
    selectedItem.Bundle = totalQuantity.ToString();

}

public void AddItemAtEmptySlot(Item item)
{

        foreach (var slot in slots)
        {
            if (slot.childCount == 0)
            {
                GameObject tmp = Resources.Load("Item") as GameObject;
                GameObject obj = Instantiate(tmp);

                GameObject parent = GameObject.Find(slot.name);
                obj.transform.SetParent(parent.transform);

                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.position = new Vector3(0, 0, 0);

                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                itemName.text = item.Name;
                itemQuantity.text = item.Bundle;
                itemIcon.sprite = item.Icon;

                break;
            }

        }

}
*/
    /*
    public void ListItemData() 
    {
        int idx = 0;
        string parentName = "";
        if (Items.Count != 0)
        {
           //Transform transform = FindItemTransform().transform;
            foreach (var item in Items)
            {
                
                if(idx == 0)
                {
                    parentName = "slot";
                }
                else
                {
                    parentName = $"slot ({idx})";
                }

                GameObject tmp = Resources.Load("Item") as GameObject;
                GameObject obj = Instantiate(tmp);

                GameObject parent = GameObject.Find(parentName);
                obj.transform.SetParent(parent.transform);

                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.position = new Vector3(0, 0, 0);

                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                itemName.text = item.Name;
                itemQuantity.text = item.Bundle;
                itemIcon.sprite = item.Icon;

  
            
                idx++;

            }
        }
    }
    

    private void ReplaceItem(Item item)
    {
        foreach(var slot in slots)
        {
            if (slot.childCount != 0)
            {
                Transform obj = slot.GetChild(0);

                var itemName = obj.Find("ItemName").GetComponent<TextMeshProUGUI>();

                if (itemName.text == item.Name)
                {
                    var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                    itemQuantity.text = item.Bundle;
                    break;
                }
            }

        }
    }
    public Item MakeSOInstance(string name, string decription, ItemType type)
    {


        if (Items.Exists(r => r.Name.Equals(name)))
        {
            Item i = Items.Find(x => x.Name == name);

            int bundle = Int32.Parse(i.Bundle);
            int q = Int32.Parse(i.Quantity);
            i.Bundle = (bundle + 1).ToString();
            i.Quantity = (q + 1).ToString();

            ReplaceItem(i);
            return i;
        }
        else
        {
            Item asset = ScriptableObject.CreateInstance<Item>();
            asset.Name = name;
            asset.Description = decription;
            asset.Bundle = "1";
            asset.Quantity = "1";
            asset.type = type;
            AddItem(asset);
            AssetDatabase.CreateAsset(asset, $"Assets/02.Scripts/08.Scriptable Object/ItemSO/{asset.Name}.asset");
            AssetDatabase.Refresh();
            AddItemAtEmptySlot(asset);

            return asset;
        }
    }
    public void ShowToolTip(Item item, Vector3 pos)
    {
        objContainer.SetActive(true);
        var itemName = objContainer.transform.Find("NameTxt").GetComponent<TextMeshProUGUI>();
        var itemDesc = objContainer.transform.Find("DescTxt").GetComponent<TextMeshProUGUI>();

        itemName.text = item.Name;
        itemDesc.text = item.Description;
        objContainer.transform.position = new Vector3(pos.x+30, pos.y-120, pos.x);

    }
    public void HideToolTip()
    {
        objContainer.SetActive(false);
    }

    */

}
