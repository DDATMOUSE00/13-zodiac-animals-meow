using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TMP_Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int bundle = 1;
    [HideInInspector] public Item item;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = item.icon;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = bundle.ToString();
        bool textActive = bundle > 1;

        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    /*
    public void OnPointerClick(PointerEventData eventData)
    {

        if (SelectedItem == null)
            FindSelectedItem();

        selectedT = this.transform;
        clickedItem = SelectedItem;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
              ItemManager.Instance.splitContainer.SetActive(true);
          //  ItemManager.Instance.SplitItem(this.transform, SelectedItem, "1");
        }


      private void FindSelectedItem()
    {

            var name = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            foreach (Item i in ItemManager.Instance.Items)
            {
                if (i.Name == name.text)
                {
                    SelectedItem = i;
                    t = this.transform;
                    break;
                }
            }
        
    }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemManager.I.ShowToolTip(item, transform.position);  
    }

  
    public void OnPointerExit(PointerEventData eventData)
    {
        ItemManager.I.HideToolTip();
    }
    */
}
