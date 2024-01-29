
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IDropHandler
{

    
    void IDropHandler.OnDrop(PointerEventData eventData)
    {

        if (transform.childCount == 0) //빈슬롯이면 
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // 현재 끄는 아이템 
            if(draggableItem!=null)
                draggableItem.parentAfterDrag = transform;
        } // 자리 옮기기만. 
        else //swap  --> 드랍한 곳에 정보가 있다면?  아이템을 바꿔야됨
      
        {

            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // 현재 끄는 아이템 
            GameObject origin = transform.GetChild(0).gameObject;
            DraggableItem originItem = origin.GetComponent<DraggableItem>(); //드랍한 곳에 있던 아이템 

            if (origin != null && dropped != null)
            {
                var DItem = dropped.transform.Find("ItemName").GetComponent<TextMeshProUGUI>(); 
                var OItem = origin.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

                if (DItem.text == OItem.text) //만약 같은 아이템이라면  이름을 판별해서 
                {
                    ItemManager.Instance.StackItem(origin, dropped);
                    Debug.Log("stack"); //합치기 
                    Destroy(dropped);
                }
                else
                { // swap 

                    originItem.parentAfterDrag = transform;
                    Transform draggableParent = draggableItem.parentAfterDrag;
                    draggableItem.parentAfterDrag = originItem.parentAfterDrag;
                    originItem.parentAfterDrag = draggableParent;
                    transform.GetChild(0).SetParent(draggableParent);
                }
            }

        }

    }
}
