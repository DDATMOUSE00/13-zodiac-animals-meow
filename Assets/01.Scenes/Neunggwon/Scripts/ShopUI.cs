using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField] private GameObject uiPrefab; 
    [SerializeField] private float space;

    [SerializeField] private ShopSlot shopSlot;
    public List<Item> items = new List<Item>();
    public List<RectTransform> UiObject = new List<RectTransform>();

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        //for (int j = 0; j < items.Count; ++j)
        //{
        //    AddNewUiObject();
        //}
        for (int i = 0; i < 3; i++)
        {
            AddNewUiObject();
        }
    }

    public void DisPlayUI()
    {
        for (int i = 0;i < items.Count; i++)
        {
            shopSlot.itemData = items[i];
        }
    }


    public void AddNewUiObject()
    {
        var newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        UiObject.Add(newUi);

        float y = 0f;

        //for (int i = 0; i < items.Count; i++)
        for (int i = 0; i < UiObject.Count; i++)
        {
            UiObject[i].anchoredPosition = new Vector2(0f, -y);
            y += UiObject[i].sizeDelta.y + space;
        }

        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }

    //public void InputNem(int num)
    //{
    //    for (int i = 0; i < num; i++)
    //    {
    //        Debug.Log($"아이템 구매 계수 {num}");
    //    }
    //}
}
