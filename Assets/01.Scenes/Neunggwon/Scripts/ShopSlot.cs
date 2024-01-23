using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour
{
    public Item itemData; //아이템 데이터를 받는다!
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemCost;

    private void Start()
    {
        Setting();
    }

    public Item ItemData(Item _itemData)
    {
        itemData = _itemData;
        return itemData;
    }

    void Setting()
    {
        itemName.text = itemData.Name;
        itemIcon.sprite = itemData.Icon;
        itemDescription.text = itemData.Description;
        //itemCost = itemData.Cost;

        if (0 == 0) // itemType에 따라 세팅해야 할 거 추가 (임시임)
        {

        }
    }
    private bool ThisEquipItam()
    {
        //아이템이 무기인지 확인...
        return false;
    }

    public void ButtonBuy()
    {
        if (!ThisEquipItam())
        {
            //갯수 입력 UI 뽕!
        }
        else
        {
            //inventory에 쏙!
            Debug.Log($"Item : {itemName} Buy");
        }
    }

    public void ButtonSell()
    {
        if (!ThisEquipItam())
        {
            //갯수 입력 UI 뽕!
        }
        else
        {
            //inventory에 쏙!
        }
    }
    


    private void InputNum(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            //inventory에 쏙! x Count
        }
    }

}
