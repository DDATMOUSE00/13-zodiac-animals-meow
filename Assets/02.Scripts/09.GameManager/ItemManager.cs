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
    public GameObject splitContainer;
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

    public void SplitItem(Transform t, Item item, string quantity)
    {
        foreach(var slot in slots)
        {
           if(slot.childCount == 0)
            {
                /*기존 quantity update*/
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


                /*새로운 슬롯아이템*/
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


    public void MakeSOInstance(string name, string decription)
    {
        Item asset = ScriptableObject.CreateInstance<Item>();

        asset.Name = name;
        asset.Description = decription;
        asset.Bundle = "1";

        if (Items.Exists(r => r.Name.Equals(name)))
        {
            Item i = Items.Find(x => x.Name == name);

            int bundle = Int32.Parse(i.Bundle);
            i.Bundle = (bundle + 1).ToString();

        }
        else
        {
            AddItem(asset);
        }

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
