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
    
    public Transform[] slots;
    public GameObject objContainer;
    public GameObject splitContainer;
    public GameObject dropBtn;
   
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


    private void AddItem(Item item)
    {

        Items.Add(item);
    }
    public bool RemoveItem(Item item) //Drop Item 
    {
        Item selectedItem = Items.Find(i => i.name == item.name);
        if (selectedItem != null)
        {
            bool IsCheckQuantityZero = (selectedItem.quantity - item.bundle == 0 ? true : false);
            if (IsCheckQuantityZero)
            {
                Debug.Log($"{item.name} removed");
              Items.Remove(item);
            }
            else
            {
                item.quantity = selectedItem.quantity - item.bundle;
            }
            return true;
        }
        return false;
    }
    public bool IsCheckItemInList(Item item)
    {
        Item selectedItem = Items.Find(i => i.name == item.name);
        if (selectedItem != null)
            return true;

        return false;
    }

    public void SplitItem(Transform t, Item item, int quantity)
    {
        foreach(var slot in slots)
        {
           if(slot.childCount == 0)
            {
                /*기존 quantity update*/
                int orginBundle = item.bundle;
                int updateBundle = orginBundle - quantity;

                if(updateBundle < 1)
                {
                    Debug.Log("실패");
                    break;
                }
                item.bundle = updateBundle;
                var originBundle = t.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                originBundle.text = item.bundle.ToString();


                /*새로운 슬롯아이템*/
                GameObject tmp = Resources.Load("Item") as GameObject;
                GameObject obj = Instantiate(tmp);
                obj.transform.SetParent(slot);
                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                obj.transform.localScale = new Vector3(1, 1, 1);
                itemName.text = item.name;
                itemIcon.sprite = item.icon;
                itemQuantity.text = quantity.ToString();

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
        Item selectedItem = Items.Find(x => x.name == item1Name.text);
        item1Quantity.text = totalQuantity.ToString();
        selectedItem.bundle = totalQuantity;
        
    }

    private void AddItemAtEmptySlot(Item item)
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
                    itemName.text = item.name;
                    itemQuantity.text = item.bundle.ToString();
                    itemIcon.sprite = item.icon;

                    break;
                }
     
            }
        
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
                itemName.text = item.name;
                itemQuantity.text = item.bundle.ToString();
                itemIcon.sprite = item.icon;

  
            
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

                if (itemName.text == item.name)
                {
                    var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                    itemQuantity.text = item.bundle.ToString();
                    break;
                }
            }

        }
    }
    public Item MakeSOInstance(string name, string decription, ItemType type)
    {
        if (Items.Exists(r => r.name.Equals(name))) // refactoring --> int로 
        {
            Item i = Items.Find(x => x.name == name); //

            int bundle = i.bundle;// 무거움 -> 리팩토링  
            int q = i.quantity; //
            i.bundle = (bundle + 1);
            i.quantity = (q + 1);

            ReplaceItem(i);
            return i;
        }
        else
        {
            Item asset = ScriptableObject.CreateInstance<Item>();
            asset.name = name;
            asset.description = decription;
            asset.bundle = 1;
            asset.quantity = 1;
            asset.type = type;
            AddItem(asset);
            AssetDatabase.CreateAsset(asset, $"Assets/02.Scripts/08.Scriptable Object/ItemSO/{asset.name}.asset"); //런타임에 저장이 안됨  / json형태로 저장 
            AssetDatabase.Refresh();
            AddItemAtEmptySlot(asset);

            return asset;
        }
    }
    public void ShowToolTip(Item item, Vector3 pos) //ray cast로 찾아보기 
    {
        objContainer.SetActive(true);
        var itemName = objContainer.transform.Find("NameTxt").GetComponent<TextMeshProUGUI>(); 
        var itemDesc = objContainer.transform.Find("DescTxt").GetComponent<TextMeshProUGUI>(); 

        itemName.text = item.name;
        itemDesc.text = item.description;
        objContainer.transform.position = new Vector3(pos.x+30, pos.y-120, pos.x);

    }
    public void HideToolTip()
    {
        objContainer.SetActive(false);
    }



}
