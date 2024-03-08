using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffChekUI : MonoBehaviour
{
    //public BaseBuff selectbuff;
    public Image buffIcon;
    public TMP_Text buffText;

    private void Start()
    {

    }

    public void SelectBuffInfo(BaseBuff selectBuff)
    {
        buffIcon.sprite = selectBuff.icon;
        //
        buffText.text = $"{selectBuff.type}�� {selectBuff.buff}���� �߽��ϴ�.";
    }
}
