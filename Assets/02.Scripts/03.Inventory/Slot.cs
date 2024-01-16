using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {

        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
        else //swap 
        {
            GameObject dropped = eventData.pointerDrag; 
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // 현재 끄는 아이템 


            GameObject origin = transform.GetChild(0).gameObject;
            DraggableItem originItem = origin.GetComponent<DraggableItem>();
            originItem.parentAfterDrag = transform;
            Transform draggableParent = draggableItem.parentAfterDrag;

            draggableItem.parentAfterDrag = originItem.parentAfterDrag;
            originItem.parentAfterDrag = draggableParent;
            transform.GetChild(0).SetParent(draggableParent); 

        }

    }
}
