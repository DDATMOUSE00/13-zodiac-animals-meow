
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {

        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // 현재 끄는 아이템 
            draggableItem.parentAfterDrag = transform;
        }
        else //swap 
        {

            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // 현재 끄는 아이템 
            var DItem = dropped.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

            GameObject origin = transform.GetChild(0).gameObject;
            DraggableItem originItem = origin.GetComponent<DraggableItem>();
           var OItem = origin.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

            Debug.Log(DItem.text);
            if (DItem.text== OItem.text)
            {
                ItemManager.Instance.StackItem(dropped, origin);
                Debug.Log("stack");
                Destroy(dropped);
            }
            else
            {

                originItem.parentAfterDrag = transform;
                Transform draggableParent = draggableItem.parentAfterDrag;

                draggableItem.parentAfterDrag = originItem.parentAfterDrag;
                originItem.parentAfterDrag = draggableParent;
                transform.GetChild(0).SetParent(draggableParent);
            }

        }

    }
}
