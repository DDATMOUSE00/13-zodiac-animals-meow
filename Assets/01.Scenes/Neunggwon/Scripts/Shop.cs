using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField] private GameObject uiPrefab;

    public List<Item> items = new List<Item>();
    public List<ShopSlot> shopSlots = new List<ShopSlot>();

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        for (int i = 0; i < items.Count - 1; i++)
        {
            AddNewUiObject();
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
            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20) * items.Count);
        }
        else
        {
            Debug.Log("Shop에 아이템이 부족합니다.");
        }
    }
}
