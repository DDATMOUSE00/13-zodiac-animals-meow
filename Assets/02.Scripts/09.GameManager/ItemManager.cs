using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemManager : MonoBehaviour
{
    private static ItemManager i;
    public List<Item> Items = new List<Item>(); //consumable  //weapon?  typeof(obj) --> consumable/ weapon?
    public List<Item> StorageItems = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Transform[] slots;
    public GameObject objContainer;
    //나중에 Weapon 추가 


    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ItemManager Instance
    {
        get
        {
            if (i == null)
            {
                return new ItemManager();
            }
            return i;
        }
    }


    public void AddItem(Item item)
    {

        Items.Add(item);
    }
    public void RemoveItem(Item item) //Drop Item 
    {
        Items.Remove(item);
    }

    public void SplitItem( Item item, string quantity)
    {
        foreach(var slot in slots)
        {
           if(slot.childCount == 0)
            {
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

                break;
                
            }  
        }    
    }
    public void StackItem(GameObject item1, GameObject item2)
    {
        var item1Quantity = item1.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
        var item2Quantity = item2.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
        int totalQuantity = Int32.Parse(item1Quantity.text) + Int32.Parse(item2Quantity.text);
        item1Quantity.text = totalQuantity.ToString();
        
    }
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
                itemName.text = item.Name;
                itemIcon.sprite = item.Icon;

                if(item.GetType() == typeof(Consumable))
                {
                    var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                    itemQuantity.text = "2";
                }
            
                idx++;

            }
        }
    }


    public void MakeSOInstance(string name, string decription)
    {
        Item asset = ScriptableObject.CreateInstance<Item>();

        asset.Name = name;
        asset.Description = decription;
   
        AddItem(asset);

        AssetDatabase.CreateAsset(asset, $"Assets/02.Scripts/08.Scriptable Object/ItemSO/{asset.Name}.asset");
        AssetDatabase.Refresh();

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



}
