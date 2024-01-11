using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager i;
    private List<Item> Items;
    public GameObject InventroyItem;
    
    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static ItemManager Instance
    {
        get
        {
            if (i == null)
            {
                return null;
            }
            return i;
        }
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void SetItemData()
    {
        Debug.Log("set item");
        if (Items.Count != 0)
        {
            Transform transform = FindItemTransform().transform;
            foreach (var item in Items)
            {
                //item1, item2, item2 
                GameObject obj = ResourceManager.Instance.Instantiate("Item", transform);
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
        AddItem(asset);
 
        AssetDatabase.CreateAsset(asset, $"Assets/Scripts/Scriptable Object/items/{asset.Name}.asset");
        AssetDatabase.Refresh();
    }

}
