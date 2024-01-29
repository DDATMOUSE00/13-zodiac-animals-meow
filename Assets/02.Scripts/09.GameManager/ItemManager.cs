using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{
    private static ItemManager i;
    public List<Item> Items = new List<Item>(); //consumable  //weapon?  typeof(obj) --> consumable/ weapon?
    
    // items ���� / �̺�Ʈ�� �߻��ϸ� json����? ��ȯ. 
    //�Ĺ��Ҷ����� �̺�Ʈ invoke 

    public Transform[] slots;
    public GameObject objContainer;
    public GameObject splitContainer;
    public GameObject dropBtn;

    private int id = 0;

    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    //���߿� Weapon �߰� 

    //1. ���ߴ��� �Ⱥ��ߴ��� ���� üũ 
    //2. �������� / �κ��丮�� ���ų� �����ư�� �������� 
    //3. �Ͻ��� ��ȭ --> �ӽ����Ͽ� ���� ����. �ð����� ���� 

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
        Item selectedItem = Items.Find(i => i.id == item.id);
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
        Item selectedItem = Items.Find(i => i.id == item.id);
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
                /*���� quantity update*/
                int orginBundle = item.bundle;
                int updateBundle = orginBundle - quantity;

                if(updateBundle < 1)
                {
                    Debug.Log("����");
                    break;
                }
                item.bundle = updateBundle;
                var originBundle = t.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                originBundle.text = item.bundle.ToString();


                /*���ο� ���Ծ�����*/
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
    public void MakeNewItem(string name, string decription, ItemType type)
    {

        Item i = Items.Find(x => x.name== name);
        if (i != null) 
        {

            int bundle = i.bundle;
            int q = i.quantity; 
            i.bundle = (bundle + 1);
            i.quantity = (q + 1);
            ReplaceItem(i);
        }
        else
        {
            AddItemAtEmptySlot(name, decription, type);

        }
    }
    private void AddItemAtEmptySlot(string name, string decription, ItemType type)
    {
        foreach (var slot in slots)
        {
            if (slot.childCount == 0)
            {
                
                GameObject obj = ResourceManager.Instance.Instantiate("Item");
                Item tmp = new Item();
                tmp.name = name;
                tmp.description = decription;
                tmp.bundle = 1;
                tmp.quantity = 1;
                tmp.type = type;
                tmp.id = id;
                tmp.parentName = slot.name;
                id++;
                AddItem(tmp);
       

                GameObject parent = GameObject.Find(slot.name);
                obj.transform.SetParent(parent.transform);

                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.position = new Vector3(0, 0, 0);

                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                var itemQuantity = obj.transform.Find("ItemBundle").GetComponent<TextMeshProUGUI>();
                itemName.text = name;
               // itemQuantity.text = "1";
                break;
            }
        }
    }


    Item res;
    public Item FindSelectedItem()
    {
        pointerEventData = new PointerEventData(EventSystem.current);  // UI�󿡼� �̺�Ʈ�� onpointer�迭�� ó��.//EventSystem ��� 
        pointerEventData.position = Input.mousePosition; //���߿��� ���콺������
        //���� ���콺 �����ǿ� �ִ°�
        raycastResults = new List<RaycastResult>();
        
        // ���� ���콺 ��ġ���� RaycastAll ����
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        GameObject selectedT = raycastResults[0].gameObject;

        //item1 -> parentName : slot 
        //item1(��������) -> parentName: slot5 / x 
        //
        foreach (var i in Items)
        {
            if (i.parentName == selectedT.transform.parent.name)
            {
                res = i;
                break;
            }
 
        }
        return res;
    }

     public void ShowToolTip(Item item, Vector3 pos)
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
