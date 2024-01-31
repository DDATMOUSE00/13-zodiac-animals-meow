using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollViewController : MonoBehaviour
{
    private ScrollRect scrollRect;
    public GameObject uiPrefab;
    public float space = 50f;

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
