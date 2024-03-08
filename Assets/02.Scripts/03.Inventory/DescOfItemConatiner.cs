using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescOfItemConatiner : MonoBehaviour
{
    public TMP_Text TMPname;
    public TMP_Text TMPdesc;

    public void Setting(Item item)
    {
        Debug.Log($"ÅøÆÁ - {TMPname.text} ");
        TMPname.text = item.itemName;
        TMPdesc.text = item.itemDescription;
    }
}
