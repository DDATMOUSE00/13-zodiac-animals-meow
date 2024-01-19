using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField] private GameObject uiPrefab; //나중에 버튼으로 변경해 ㅇㅋ?
    [SerializeField] private float space;
    //public ItemData[] itemDatas;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI Description;

    public List<RectTransform> UiObject = new List<RectTransform>();

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            AddNewUiObject();
        }
    }

    public void DisplayItemData()
    {
        //itemData의 내용을 받아 UI에 보이도록 하자 ㄱㄱ
    }


    public void AddNewUiObject()
    {
        var newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        UiObject.Add(newUi);

        float y = 0f;

        for (int i = 0; i < UiObject.Count; i++)
        {
            UiObject[i].anchoredPosition = new Vector2(0f, -y);
            y += UiObject[i].sizeDelta.y + space;
        }

        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }
}
