using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour
{
    public TMP_InputField userTxt;
    public void ConfirmSplit()
    {
        ItemManager.Instance.SplitItem(DraggableItem.selectedT, DraggableItem.clickedItem, userTxt.text);
    }

    public void DropClickedItem()
    {
        if (DraggableItem.clickedItem != null)
        {
            Debug.Log($"{DraggableItem.clickedItem.Name} : 버리기");
            ItemManager.Instance.RemoveItem(DraggableItem.clickedItem);
            Destroy(DraggableItem.selectedT.gameObject);

            /*
            for (int i = 0; i < Int32.Parse(DraggableItem.clickedItem.Bundle) ; i++)
            {
                //Instantiate();//prefab;
            }
            */

        }
        else
        {
            Debug.Log("버릴 아이템을 선택해주세요");
        }

    }


}
