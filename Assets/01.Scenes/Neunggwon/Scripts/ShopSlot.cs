using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour
{
    [Header("#ItemDataInfo")]
    public Item itemData; //������ �����͸� �޴´�!
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemCost;

    [Header("#InputFeild")]
    [SerializeField] private GameObject inputFeild;
    public InputField inputField;
    public string inputNum = null;

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
    private bool ThisEquipItam() //test
    {
        //�������� �������� Ȯ��...
        return false;
    }

    public void ButtonBuy()
    {
        if (!ThisEquipItam())
        {
            //���� �Է� UI ��!
            inputFeild.SetActive(true);
        }
        else
        {
            //inventory�� ��!
            //InventoryManager
            //AddItem(this.itemData);
            Debug.Log($"Item : {itemName} Buy");
        }
    }

    //public void ButtonSell()
    //{
    //    if (!ThisEquipItam())
    //    {
    //        //���� �Է� UI ��!
    //    }
    //    else
    //    {
    //        //inventory�� ��!
    //    }
    //}
    

    public void InputNum()
    {
        int inputNum = int.Parse(inputField.text);
        ///<summary>
        ///�ӽ�..
        ///�÷��̾� ��� Ȯ��
        /// </summary>
        
        //if (PlayerStatus.Gold >= itemData.Cost * inputNum)
        //{
        //    PlayerStatus.Gold -= (itemData.Cost * inputNum)
        //    AddItem(this.itemData, inputNum)
        //}
        //else
        //{
        //    Debug.Log($"�÷��̾� ��� ����!!");
        //}

    }
}
