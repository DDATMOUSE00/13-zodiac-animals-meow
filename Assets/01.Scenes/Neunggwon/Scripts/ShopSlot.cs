using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour
{
    public Item itemData; //������ �����͸� �޴´�!
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemCost;

    private void Start()
    {
        Setting();
    }

    public Item ItemData(Item _itemData)
    {
        itemData = _itemData;
        return itemData;
    }

    void Setting()
    {
        itemName.text = itemData.Name;
        itemIcon.sprite = itemData.Icon;
        itemDescription.text = itemData.Description;
        //itemCost = itemData.Cost;

        if (0 == 0) // itemType�� ���� �����ؾ� �� �� �߰� (�ӽ���)
        {

        }
    }
    private bool ThisEquipItam()
    {
        //�������� �������� Ȯ��...
        return false;
    }

    public void ButtonBuy()
    {
        if (!ThisEquipItam())
        {
            //���� �Է� UI ��!
        }
        else
        {
            //inventory�� ��!
            Debug.Log($"Item : {itemName} Buy");
        }
    }

    public void ButtonSell()
    {
        if (!ThisEquipItam())
        {
            //���� �Է� UI ��!
        }
        else
        {
            //inventory�� ��!
        }
    }
    


    private void InputNum(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            //inventory�� ��! x Count
        }
    }

}
