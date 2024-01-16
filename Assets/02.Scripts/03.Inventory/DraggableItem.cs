using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {

        var name = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        foreach(Item i in ItemManager.Instance.Items)
        {
            if(i.Name == name.text)
            {
                SelectedItem = i;
                break;

    
            }
        }
       
        ItemManager.Instance.ShowToolTip(SelectedItem, transform.position);
       
    }


        public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        ItemManager.Instance.HideToolTip();
    }
}
