using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField] private GameObject uiPrefab; 

    public List<Item> items = new List<Item>();
    public List<ShopSlotUI> shopSlots = new List<ShopSlotUI>();

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            AddNewUiObject();
        }
    }

    public void AddNewUiObject()
    {
        var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlotUI>();
        shopSlots.Add(newShopSlot);

        float y = 150f;

        for (int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].itemData = items[i];
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20)* items.Count);
    }
}
