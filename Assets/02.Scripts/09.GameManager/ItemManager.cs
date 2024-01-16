using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    private static ItemManager i;
    public List<Item> Items = new List<Item>(); //consumable  //weapon?  typeof(obj) --> consumable/ weapon?
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Slot[] slots;
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
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
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
            
                idx++;

            }
        }
    }
    private GameObject FindItemTransform()
    {
        GameObject itemObj = GameObject.Find("@Item");
        if (itemObj == null) itemObj = new GameObject { name = "@Item" };
        return itemObj;
    }

    public void MakeSOInstance(string name)
    {
        Item asset = ScriptableObject.CreateInstance<Item>();


        asset.Name = name;
        asset.Description = name;
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
