using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
   [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    public TMP_Text desc;
    private Item SelectedItem;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SelectedItem == null)
            FindSelectedItem();
        ItemManager.Instance.splitContainer.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (SelectedItem == null)
            FindSelectedItem();
        ItemManager.Instance.ShowToolTip(SelectedItem, transform.position);  
    }

    private void FindSelectedItem()
    {
        var name = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        foreach (Item i in ItemManager.Instance.Items)
        {
            if (i.Name == name.text)
            {
                SelectedItem = i;
                break;
            }
        }
    }

        public void OnPointerExit(PointerEventData eventData)
    {

        ItemManager.Instance.HideToolTip();
    }
}
