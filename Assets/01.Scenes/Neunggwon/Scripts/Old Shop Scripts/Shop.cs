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

    //Inventory�� ������ ������ ����!
    public bool IsBuyShop;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    private void OnEnable()
    {
        for (int i = 0; i < items.Count; i++)
        {
            AddNewUiObject();
        }
    }

    private void Start()
    {
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
            Debug.Log("Shop�� �������� �����մϴ�.");
        }
    }
}