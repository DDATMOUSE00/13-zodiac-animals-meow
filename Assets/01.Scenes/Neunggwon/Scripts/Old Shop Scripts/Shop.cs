using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField] private GameObject uiPrefab;

    public List<Item> items = new List<Item>();
    public List<ShopSlot> shopSlots = new List<ShopSlot>();

    //Inventory의 slot의 정보를 가지고 오자!
    public bool IsSellShop;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    private void OnEnable()
    {

    }

    private void GetInventory()
    {
        //items가 inventory의 List를 바라보도록 하자!!
        Test_Inventory inventory = new Test_Inventory();
        items = inventory.items;
    }


    private void Start()
    {
        if (!IsSellShop)
        {
            for (int i = 0; i < items.Count; i++)
            {
                AddNewUiObject();
                //shopSlots[i].ButtonBuy();
            }
        }
        else
        {
            //for (int i = 0; i < Inventory.Instance.slots.Count; i++)
            //{
            //    AddNewUiObject();
            //}
            Debug.Log("Inventory의 Slots의 정보를 가지고 오자!!");
        }
    }


    public void AddNewUiObject()
    {
        if (shopSlots.Count < items.Count)
        {
            var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
            shopSlots.Add(newShopSlot);

            float y = 150f;

            for (int i = 0; i < shopSlots.Count; i++)
            {
                shopSlots[i].itemData = items[i];
            }
            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20) * shopSlots.Count);
        }
        else
        {
            Debug.Log("Shop에 아이템이 부족합니다.");
        }
    }
}
