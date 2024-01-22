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

    /*상태창*/
    public  Item SelectedItem;
    public  Transform t;

    /*버릴때*/
    public static Item clickedItem;
    public static Transform selectedT;

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

        selectedT = this.transform;
        clickedItem = SelectedItem;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
              ItemManager.Instance.splitContainer.SetActive(true);
          //  ItemManager.Instance.SplitItem(this.transform, SelectedItem, "1");
        }


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
                    t = this.transform;
                    break;
                }
            }
        
    }
        public void OnPointerExit(PointerEventData eventData)
    {

        ItemManager.Instance.HideToolTip();
    }
}
