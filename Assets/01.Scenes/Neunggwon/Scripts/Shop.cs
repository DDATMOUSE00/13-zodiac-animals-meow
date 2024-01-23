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
    [SerializeField] private float space;

    public List<Item> items = new List<Item>();
    public List<ShopSlot> shopSlots = new List<ShopSlot>();
    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            AddNewUiObject1();
            
        }
    }

    //public void AddNewUiObject(int index)
    //{
    //    var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
    //    shopSlots.Add(newShopSlot);
    //    shopSlots[index].itemData = items[index];

    //    float y = 0f;
    //    float x = 660f;

    //    //for (int i = 0; i < items.Count; i++)
    //    for (int i = 0; i < shopSlots.Count; i++)
    //    {
    //        //shopSlots[i].transform.position = new Vector2(x, -y);
    //        y = 150 + space;
    //        Debug.Log($"{index}번짼 y : {y}");

    //        //Debug.Log($"{index} transform.position: {shopSlots[i].transform.position}");
    //        //space 거리
    //        //UiObject[i].transform.position.anchoredPosition = new Vector2(0f, -y);
    //        //y += UiObject[i].sizeDelta.y + space;
    //    }
    //    shopSlots[index].transform.position = new Vector2(x, -y);
    //    //Debug.Log($"{index} transform.position: {shopSlots[index].transform.position}");

    //    //shopSlots[index].transform.position = new Vector2(x, -y);
    //    //y = 150 + space;

    //    scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y * items.Count);
    //}
    public void Button_Test()
    {
        if (shopSlots.Count < items.Count)
        {
            AddNewUiObject1();
        }
        else
        {
            Debug.Log("ItemData가 없습니다.");
        }
    }

    public void AddNewUiObject1()
    {
        var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
        //var newShopSlot_Rec = scrollRect.content.GetComponent<RectTransform>();
        //scrollRect.content.GetComponent<RectTransform>();//test
        shopSlots.Add(newShopSlot);

        float defaultX = 660;
        float defaultY = 190;
        float y = 0f;
        //shopSlots[shopSlots.Count].transform.position = new Vector2(x, -190f);

        for (int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].itemData = items[i];
            shopSlots[i].transform.position = new Vector2(defaultX, defaultY - y);
            y += 150 + space;

        }

        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }


}
