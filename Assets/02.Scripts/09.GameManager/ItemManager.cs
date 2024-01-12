using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private static ItemManager i;
    private List<Consumable> Items = new List<Consumable>(); 
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

    public void SetItemData() 
    {
        Debug.Log("set item");
        if (Items.Count != 0)
        {
            Transform transform = FindItemTransform().transform;
            foreach (var item in Items)
            {
                Debug.Log(item.Name);
                GameObject obj = ResourceManager.Instance.Instantiate("Prefabs/Item.prefab", transform);
                var itemBundle = obj.transform.Find("ItemBundle").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                itemBundle.text = item.Bundle;
                itemIcon.sprite = item.Icon;
            }
        }
    }
    private GameObject FindItemTransform()
    {
        GameObject itemObj = GameObject.Find("@Item");
        if (itemObj == null) itemObj = new GameObject { name = "@Item" };
        return itemObj;
    }

    public void MakeSOInstance(string name, string bundle)
    {
        Consumable asset = ScriptableObject.CreateInstance<Consumable>();

        asset.Name = name;
        asset.Bundle = bundle;

        AddConsumableItem(asset);
 
        AssetDatabase.CreateAsset(asset, $"Assets/02.Scripts/08.Scriptable Object/ItemSO/{asset.Name}.asset");
        AssetDatabase.Refresh();

    }

}
