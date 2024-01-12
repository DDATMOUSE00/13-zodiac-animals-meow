using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    private static ItemManager i;
    public List<Consumable> Items = new List<Consumable>();
    public Transform ItemContent;
    public GameObject InventoryItem;
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

    public void AddConsumableItem(Consumable item)
    {

        Items.Add(item);
    }
    public void RemoveConsumableItem(Consumable item)
    {
        Items.Remove(item);
    }

    public void ListItemData() 
    {
        if (Items.Count != 0)
        {
            Transform transform = FindItemTransform().transform;
            foreach (var item in Items)
            {

               
                GameObject obj = ResourceManager.Instance.Instantiate("Item.prefab", transform);
                Debug.Log(item.Name);
                var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
               // var itemName= obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
               // var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                itemName.text = item.Name;
               // itemIcon.sprite = item.Icon;
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
        Consumable asset = ScriptableObject.CreateInstance<Consumable>();


        asset.Name = name;
        AddConsumableItem(asset);
 
        AssetDatabase.CreateAsset(asset, $"Assets/02.Scripts/08.Scriptable Object/ItemSO/{asset.Name}.asset");
        AssetDatabase.Refresh();

    }

}
