using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlot : MonoBehaviour
{
    public Item itemData;
    public int Count;

    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemIcon;
    //[SerializeField] private ItemType itemType;
    
    void Start()
    {
        Setting();
    }

    private void Setting()
    {
        itemData.name = itemName;
        itemData.Description = itemDescription;
        itemData.Icon = itemIcon;
    }

    public void Button_Click()
    {
        if (itemData != null)
        {
            Test_Inventory.Instence.SelectItem(this);
        }
        else
        {
            Debug.Log("비어있는 슬롯 입니다.");
        }
    }
    
}
