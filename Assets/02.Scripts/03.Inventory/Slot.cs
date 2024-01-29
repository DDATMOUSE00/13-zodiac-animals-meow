
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IDropHandler
{

    
    void IDropHandler.OnDrop(PointerEventData eventData)
    {

        if (transform.childCount == 0) //�󽽷��̸� 
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // ���� ���� ������ 
            if(draggableItem!=null)
                draggableItem.parentAfterDrag = transform;
        } // �ڸ� �ű�⸸. 
        else //swap  --> ����� ���� ������ �ִٸ�?  �������� �ٲ�ߵ�
      
        {

            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // ���� ���� ������ 
            GameObject origin = transform.GetChild(0).gameObject;
            DraggableItem originItem = origin.GetComponent<DraggableItem>(); //����� ���� �ִ� ������ 

            if (origin != null && dropped != null)
            {
                var DItem = dropped.transform.Find("ItemName").GetComponent<TextMeshProUGUI>(); 
                var OItem = origin.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

                if (DItem.text == OItem.text) //���� ���� �������̶��  �̸��� �Ǻ��ؼ� 
                {
                    ItemManager.Instance.StackItem(origin, dropped);
                    Debug.Log("stack"); //��ġ�� 
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
