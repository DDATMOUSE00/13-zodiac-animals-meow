using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class SplitItemController : MonoBehaviour
{
    public TMP_InputField userTxt;
    public void ConfirmSplit()
    {
        ItemManager.Instance.SplitItem(DraggableItem.selectedT, DraggableItem.clickedItem, userTxt.text);
    }


}
